using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpNeat;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;
using SharpNeat.Phenomes;
using SharpNeat.Domains;
using SharpNeat.Core;
using System.Xml;
using System.Runtime.InteropServices;
using SharpNeat.DistanceMetrics;
using SharpNeat.SpeciationStrategies;
using SharpNeat.EvolutionAlgorithms.ComplexityRegulation;
using SharpNeat.Decoders.Neat;
using AliveAPIDotNet.DebugHelpers;
using System.Drawing;
using SharpNeat.Network;
using System.Threading;

namespace AliveAPIDotNet.AI
{
    public class AIPlayer : OnScreenRenderHelper
    {
        QuikSave startSave;

        NeatEvolutionAlgorithm<NeatGenome> _ea;
        AbeEvaluator AbeEval = new AbeEvaluator();

        int inputsCount = 2;
        int outputsCount = 4; // Up, Down, Left, Right, Chant, Hop

        bool FrameSkip { get { return Marshal.ReadByte(new IntPtr(0x005CA4D1)) == 1; } set { Marshal.WriteByte(new IntPtr(0x005CA4D1), (byte)((value) ? 1 : 0)); } }

        void CreateAI()
        {
            NeatEvolutionAlgorithmParameters param = new NeatEvolutionAlgorithmParameters();

            IDistanceMetric distanceMetric = new ManhattanDistanceMetric(1.0, 0.0, 10.0);
            ISpeciationStrategy<NeatGenome> speciationStrategy = new ParallelKMeansClusteringStrategy<NeatGenome>(distanceMetric);
            IComplexityRegulationStrategy complexityRegulationStrategy = ExperimentUtils.CreateComplexityRegulationStrategy("Absolute", 50);
            

            _ea = new NeatEvolutionAlgorithm<NeatGenome>(param, speciationStrategy, complexityRegulationStrategy);
            

            IGenomeDecoder<NeatGenome, IBlackBox> genomeDecoder = new NeatGenomeDecoder(SharpNeat.Decoders.NetworkActivationScheme.CreateCyclicFixedTimestepsScheme(1));
            IGenomeListEvaluator<NeatGenome> genomeListEvaluator = new ParallelGenomeListEvaluator<NeatGenome, IBlackBox>(genomeDecoder, AbeEval);

            //genomeListEvaluator = new SelectiveGenomeListEvaluator<NeatGenome>(genomeListEvaluator,
            //                             SelectiveGenomeListEvaluator<NeatGenome>.CreatePredicate_OnceOnly());

            IGenomeFactory<NeatGenome> genomeFactory = new NeatGenomeFactory(inputsCount, outputsCount);

            // Initialize the evolution algorithm.
            _ea.Initialize(genomeListEvaluator, genomeFactory, genomeFactory.CreateGenomeList(20, 0));

            _ea.UpdateScheme = new UpdateScheme(1);
        }

        public void Start()
        {
            startSave = new QuikSave(File.ReadAllBytes("neat.sav"));
            AliveAPI.QuikLoad(startSave);

            FrameSkip = true;

            CreateAI();
            _ea.StartContinue();

            AliveAPI.GameTick += AliveAPI_GameTick;
            AliveAPI.OnInput += AliveAPI_OnInput;
        }

        private void AliveAPI_OnInput(object sender, InputEventArgs e)
        {
            if (e.Index == 0)
            {
                Console.WriteLine(e.Pad.Pressed.Value.ToString("X"));
                //if (AbeEval.Outputs[0] > 0.5)
                //{
                //    e.Pad.Pressed.Value |= 0x4;
                //}
                //if (AbeEval.Outputs[1] > 0.5)
                //{
                //    e.Pad.Pressed.Value |= 0x8;
                //}
                //if (AbeEval.Outputs[2] > 0.5)
                //{
                //    e.Pad.Pressed.Value |= 0x1;
                //}
                //if (AbeEval.Outputs[3] > 0.5)
                //{
                //    e.Pad.Pressed.Value |= 0x2;
                //}
                //if (AbeEval.Outputs[4] > 0.5)
                //{
                //    e.Pad.Pressed.Value |= 0x40000;
                //}
                //if (AbeEval.Outputs[5] > 0.5)
                //{
                //    e.Pad.Pressed.Value |= 0x100;
                //}

                if (AbeEval.Outputs[0] > 0.5)
                {
                    e.Pad.Pressed.Value |= 0x4;
                }
                if (AbeEval.Outputs[1] > 0.5)
                {
                    e.Pad.Pressed.Value |= 0x8;
                }
                if (AbeEval.Outputs[2] > 0.5)
                {
                    e.Pad.Pressed.Value |= 0x100;
                }
                if (AbeEval.Outputs[3] > 0.5)
                {
                    e.Pad.Pressed.Value |= 0x1;
                }
            }
        }

        private void AliveAPI_GameTick(object sender, EventArgs e)
        {
            AbeEval.ThreadWait = true;
            if (AliveAPI.GetPlayerObject() != null)
            {
                AbeEval.TargetX = (int)AliveAPI.GetPlayerObject().PositionX;
            }

            Console.WriteLine($"Generation: {_ea.CurrentGeneration} Genome Count: {_ea.GenomeList.Count} Current Score: {AbeEval._points} Best Fitness {_ea.CurrentChampGenome.EvaluationInfo.Fitness}");

            //while (_ea.RunState == RunState.Running)
            //{

            //}

            if (AbeEval.StopConditionSatisfied)
            {
                AliveAPI.QuikLoad(startSave);
                Console.WriteLine("self reset");
                AbeEval.Reset();
                _ea.StartContinue();
            }
        }

        struct NodePos
        {
            public INetworkNode Node;
            public int X;
            public int Y;
        }

        public override void OnRender(Graphics g)
        {
            try
            {
                List<NodePos> nodePosList = new List<NodePos>();

                var nl = _ea.CurrentChampGenome.NodeList;
                var cl = _ea.CurrentChampGenome.ConnectionList;

                int ic = 0;
                int oc = 0;
                int hn = 0;
                int bn = 0;

                foreach (var n in nl)
                {
                    int x = 0;
                    int y = 0;

                    if (n.NodeType == NodeType.Input)
                    {
                        ic++;
                        x = 0;
                        y = ic * 8;
                    }
                    else if (n.NodeType == NodeType.Output)
                    {
                        oc++;
                        x = 100;
                        y = oc * 8;
                    }
                    else if (n.NodeType == NodeType.Hidden)
                    {
                        hn++;
                        x = 50;
                        y = hn * 8;
                    }

                    nodePosList.Add(new NodePos() { Node = n, X = x, Y = y });
                }
                foreach (var c in nodePosList)
                {
                    g.FillRectangle(Brushes.White, new RectangleF(c.X, c.Y, 6, 3));
                }

                foreach (var c in cl)
                {
                    var srcNode = nodePosList.First(x => x.Node.Id == c.SourceNodeId);
                    var dstNode = nodePosList.First(x => x.Node.Id == c.TargetNodeId);
                    g.DrawLine((c.Weight > 0.5) ? Pens.Red : Pens.Green, new Point(srcNode.X, srcNode.Y), new Point(dstNode.X, dstNode.Y));
                }
            }
            catch
            {

            }
            
        }
    }

    public class AbeEvaluator : IPhenomeEvaluator<IBlackBox>
    {
        private ulong _evalCount = 0;
        private bool _stopConditionSatisfied;

        public ulong EvaluationCount => _evalCount;

        public bool StopConditionSatisfied => _stopConditionSatisfied;

        int idleTime = 0;

        int prevTargetX = 0;
        public int TargetX = 0;

        int previousFrame = 0;
        int prevMudsSaved = 0;
        public int _points = 0;
        bool firstInit = true;

        public bool ThreadWait = false;

        public double[] Outputs;

        public FitnessInfo Evaluate(IBlackBox phenome)
        {
            int currentMudsSaved = Marshal.ReadInt16(new IntPtr(0x005C1BC2));

            if (firstInit)
            {
                firstInit = false;
                prevTargetX = TargetX;
                previousFrame = AliveAPI.gnFrame;
                prevMudsSaved = currentMudsSaved;
            }
            

            var abe = AliveAPI.GetPlayerObject();

            if (abe != null)
            {
                phenome.InputSignalArray[0] = (AliveAPI.Raycast((int)abe.PositionXRaw - (25 << 16), (int)abe.PositionYRaw, (int)abe.PositionXRaw - (25 << 16), (int)abe.PositionYRaw + (64 << 16), 7)) ? 1 : 0;
                phenome.InputSignalArray[1] = (AliveAPI.Raycast((int)abe.PositionXRaw + (25 << 16), (int)abe.PositionYRaw, (int)abe.PositionXRaw + (25 << 16), (int)abe.PositionYRaw + (64 << 16), 7)) ? 1 : 0;
            }

            phenome.Activate();

            Outputs = new double[phenome.OutputCount];
            phenome.OutputSignalArray.CopyTo(Outputs, 0);

            if (ThreadWait)
            {
                while (previousFrame == AliveAPI.gnFrame)
                {
                    Thread.Sleep(1);
                }
            }

            _evalCount++;

            int saveMudsDelta = currentMudsSaved - prevMudsSaved;

            if (prevTargetX == TargetX)
            {
                idleTime++;
                if (idleTime > 1000)
                {
                    _stopConditionSatisfied = true;
                }
            }
            else
                idleTime = 0;

            _points += TargetX - prevTargetX;
            //_points += saveMudsDelta * 5000;

            prevTargetX = TargetX;
            previousFrame = AliveAPI.gnFrame;
            prevMudsSaved = currentMudsSaved;

            return new FitnessInfo(_points, _points);
        }

        public void Reset()
        {
            Console.WriteLine("reset");
            _evalCount = 0;
            _stopConditionSatisfied = false;
            idleTime = 0;
            firstInit = true; ;
        }
    }
}
