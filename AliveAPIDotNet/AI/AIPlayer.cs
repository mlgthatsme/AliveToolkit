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

        int inputsCount = 9;
        int outputsCount = 5; // Up, Down, Left, Right, Chant, Hop

        bool FrameSkip { get { return Marshal.ReadByte(new IntPtr(0x005CA4D1)) == 1; } set { Marshal.WriteByte(new IntPtr(0x005CA4D1), (byte)((value) ? 1 : 0)); } }

        void CreateAI()
        {
            NeatEvolutionAlgorithmParameters param = new NeatEvolutionAlgorithmParameters();

            IDistanceMetric distanceMetric = new ManhattanDistanceMetric(1.0, 0.0, 10.0);
            ISpeciationStrategy<NeatGenome> speciationStrategy = new ParallelKMeansClusteringStrategy<NeatGenome>(distanceMetric);
            IComplexityRegulationStrategy complexityRegulationStrategy = ExperimentUtils.CreateComplexityRegulationStrategy("Absolute", 30);
            

            _ea = new NeatEvolutionAlgorithm<NeatGenome>(param, speciationStrategy, complexityRegulationStrategy);
            

            IGenomeDecoder<NeatGenome, IBlackBox> genomeDecoder = new NeatGenomeDecoder(SharpNeat.Decoders.NetworkActivationScheme.CreateCyclicFixedTimestepsScheme(1));
            IGenomeListEvaluator<NeatGenome> genomeListEvaluator = new ParallelGenomeListEvaluator<NeatGenome, IBlackBox>(genomeDecoder, AbeEval);

            //genomeListEvaluator = new SelectiveGenomeListEvaluator<NeatGenome>(genomeListEvaluator,
            //                             SelectiveGenomeListEvaluator<NeatGenome>.CreatePredicate_OnceOnly());

            IGenomeFactory<NeatGenome> genomeFactory = new NeatGenomeFactory(inputsCount, outputsCount);

            // Initialize the evolution algorithm.
            _ea.Initialize(genomeListEvaluator, genomeFactory, genomeFactory.CreateGenomeList(20, 0));
        }

        public void Start()
        {
            if (File.Exists("neat.sav"))
            {
                startSave = new QuikSave(File.ReadAllBytes("neat.sav"));
                AliveAPI.QuikLoad(startSave);

                FrameSkip = true;

                CreateAI();


                AliveAPI.GameTick += AliveAPI_GameTick;
                AliveAPI.OnInput += AliveAPI_OnInput;
            }
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
                    //e.Pad.Pressed.Value |= 0x1;
                }
                if (AbeEval.Outputs[4] > 0.5)
                {
                    e.Pad.Pressed.Value |= 0x40000;
                }
            }
        }

        bool started = false;

        private void AliveAPI_GameTick(object sender, EventArgs e)
        {
            if (!started)
            {
                started = true;
                _ea.StartContinue();
                AbeEval.IsReady = true;
            }

            Console.WriteLine($"Generation: {_ea.CurrentGeneration} Specie Size: {_ea.Statistics._minSpecieSize} Current Score: {AbeEval._points} Best Fitness {_ea.CurrentChampGenome.EvaluationInfo.Fitness}");

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
                    Brush color = Brushes.White;
                    if (c.Node.NodeType == NodeType.Input && AbeEval.Inputs[c.Node.Id] > 0)
                        color = Brushes.Yellow;
                            
                    g.FillRectangle(color, new RectangleF(c.X, c.Y, 6, 3));
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
        public AbeEvaluator()
        {
            Outputs = new double[100];
            Inputs = new double[100];
        }
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
        byte[] switchStatePrev;

        public bool IsReady = false;

        public double[] Outputs;
        public double[] Inputs;

        bool IsObjectNearPoint(int x ,int y, int objectType)
        {
            foreach (var w in AliveAPI.ObjectList.AsAliveObjects.Where(z => z.ObjectID == objectType))
            {
                if (x > w.PositionX - 15 && x < w.PositionX + 15 && y > w.PositionY - 15 && y < w.PositionY + 15)
                {
                    return true;
                }
            }

            return false;
        }

        public FitnessInfo Evaluate(IBlackBox phenome)
        {
            if (!IsReady)
                return new FitnessInfo(0, 0);

            while (AliveAPI.GetPlayerObject() == null)
            {
                Thread.Sleep(1);
            }

            prevTargetX = 0;
            previousFrame = AliveAPI.gnFrame;
            prevMudsSaved = Marshal.ReadInt16(new IntPtr(0x005C1BC2));
            switchStatePrev = AliveAPI.GetSwitchStates();

            while (!StopConditionSatisfied)
            {
                while (previousFrame == AliveAPI.gnFrame)
                {
                    Thread.Sleep(1);
                }
                
                byte[] switchStates = AliveAPI.GetSwitchStates();
                var abe = AliveAPI.GetPlayerObject();

                int currentMudsSaved = Marshal.ReadInt16(new IntPtr(0x005C1BC2));

                TargetX = (int)abe.PositionX;

                if (abe != null)
                {
                    var objs = AliveAPI.ObjectList.AsAliveObjects;

                    phenome.InputSignalArray[0] = (AliveAPI.Raycast((int)abe.PositionXRaw - (25 << 16), (int)abe.PositionYRaw, (int)abe.PositionXRaw - (25 << 16), (int)abe.PositionYRaw + (64 << 16), 7)) ? 1 : 0;
                    phenome.InputSignalArray[1] = (AliveAPI.Raycast((int)abe.PositionXRaw + (25 << 16), (int)abe.PositionYRaw, (int)abe.PositionXRaw + (25 << 16), (int)abe.PositionYRaw + (64 << 16), 7)) ? 1 : 0;
                    phenome.InputSignalArray[2] = (abe.AliveState == 58) ? 1 : 0; // Is on Ledge
                    phenome.InputSignalArray[3] = (objs.Where(x => x.ObjectID == 35).Count() > 0) ? 1 : 0; // Birds Exist

                    phenome.InputSignalArray[4] = (IsObjectNearPoint((int)abe.PositionX, (int)abe.PositionY, 148)) ? 1 : 0;

                    // Bombs two spaces forward and back.
                    phenome.InputSignalArray[5] = (IsObjectNearPoint((int)abe.PositionX - 25, (int)abe.PositionY, 88)) ? 1 : 0;
                    phenome.InputSignalArray[6] = (IsObjectNearPoint((int)abe.PositionX + 25, (int)abe.PositionY, 88)) ? 1 : 0;
                    phenome.InputSignalArray[7] = (IsObjectNearPoint((int)abe.PositionX - 50, (int)abe.PositionY, 88)) ? 1 : 0;
                    phenome.InputSignalArray[8] = (IsObjectNearPoint((int)abe.PositionX + 50, (int)abe.PositionY, 88)) ? 1 : 0;
                }

                phenome.Activate();

                Outputs = new double[phenome.OutputCount];
                Inputs = new double[phenome.InputCount];
                phenome.OutputSignalArray.CopyTo(Outputs, 0);
                phenome.InputSignalArray.CopyTo(Inputs, 0);

                _evalCount++;

                int saveMudsDelta = currentMudsSaved - prevMudsSaved;

                if (prevTargetX == TargetX)
                {
                    idleTime++;
                    if (idleTime > 1000)
                    {
                        _points = -100000;
                        _stopConditionSatisfied = true;
                    }
                }
                else
                    idleTime = 0;

                int abeHealth = abe.SafeReadInt32(abe.mAddress + 0x10c);

                if (abeHealth == 0)
                    _stopConditionSatisfied = true;

                //_points += TargetX - prevTargetX;

                if (saveMudsDelta > 0)
                    _points += saveMudsDelta * 500;

                if (switchStatePrev != null)
                {
                    for(int i = 0; i < switchStates.Length;i++)
                    {
                        if (switchStates[i] != switchStatePrev[i] && switchStates[i] > 0)
                            _points += 100;
                    }
                }

                prevTargetX = TargetX;
                previousFrame = AliveAPI.gnFrame;
                prevMudsSaved = currentMudsSaved;
                switchStatePrev = switchStates;
            }

            _points += (int)AliveAPI.GetPlayerObject().PositionX;
            if (_points < 0)
                _points = 0;

            return new FitnessInfo(_points, _points);
        }

        public void Reset()
        {
            Console.WriteLine("reset");
            _evalCount = 0;
            _stopConditionSatisfied = false;
            idleTime = 0;
            _points = 0;
        }
    }
}
