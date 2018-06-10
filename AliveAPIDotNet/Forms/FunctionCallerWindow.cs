using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Assembly.CallingConvention;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliveAPIDotNet.Forms
{
    public partial class FunctionCallerWindow : Form
    {
        public FunctionCallerWindow()
        {
            InitializeComponent();

            AliveAPI.GameTick += AliveAPI_GameTick;
            label1.Text = "";
            mMemSharp = new MemorySharp(Process.GetCurrentProcess());
        }

        bool bCallFunction = false;
        MemorySharp mMemSharp;

        public static string[] SplitCSV(string input)
        {
            Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart(','));
            }

            return list.ToArray();
        }

        void DecodeInstructionLine(string line)
        {
            var callingConvention = (CallingConventions)Enum.Parse(typeof(CallingConventions), comboBox1.Text);
            // Decode input
            var inputText = line;

            // ~|Cdecl !|Thiscall #|Stdcall $|Fastcall

            if (inputText.EndsWith("~"))
            {
                inputText = inputText.Substring(0, inputText.Length - 1);
                callingConvention = CallingConventions.Cdecl;
            }
            else if (inputText.EndsWith("!"))
            {
                inputText = inputText.Substring(0, inputText.Length - 1);
                callingConvention = CallingConventions.Thiscall;
            }
            else if (inputText.EndsWith("#"))
            {
                inputText = inputText.Substring(0, inputText.Length - 1);
                callingConvention = CallingConventions.Stdcall;
            }
            else if (inputText.EndsWith("$"))
            {
                inputText = inputText.Substring(0, inputText.Length - 1);
                callingConvention = CallingConventions.Fastcall;
            }

            if (inputText.EndsWith(";"))
                inputText = inputText.Substring(0, inputText.Length - 1);

            var nameVarSplit = inputText.Split(new char[] { '(' }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (nameVarSplit[1].EndsWith(")"))
                nameVarSplit[1] = nameVarSplit[1].Substring(0, nameVarSplit[1].Length - 1);

            var args = SplitCSV(nameVarSplit[1]).Where(x => x != "").ToArray();

            List<object> paramList = new List<object>();

            foreach (var a in args)
            {
                string numstr = a;

                if (numstr.StartsWith(" "))
                    numstr = numstr.Substring(1, numstr.Length - 1);
                if (numstr.EndsWith(" "))
                    numstr = numstr.Substring(0, numstr.Length - 1);

                if (numstr.StartsWith("\"") && numstr.EndsWith("\"")) // string
                {
                    IntPtr strPtr = Marshal.StringToHGlobalAnsi(numstr.Substring(1, numstr.Length - 2));
                    paramList.Add(strPtr);
                }
                else if (numstr.StartsWith("0x")) // hex int
                {
                    paramList.Add(int.Parse(numstr.Substring(2, numstr.Length - 2), System.Globalization.NumberStyles.HexNumber));
                }
                else if (numstr.ToLower().EndsWith("f")) // float
                {
                    paramList.Add(float.Parse(numstr.Substring(0, numstr.Length - 1)));
                }
                else // int
                {
                    paramList.Add(int.Parse(numstr));
                }
            }


            label1.Text = "";

            dynamic[] p = paramList.Cast<object>().ToArray();

            string addressString = nameVarSplit[0];
            if (addressString.Contains("_"))
                addressString = addressString.Split('_').Last();

            IntPtr addr = new IntPtr(int.Parse(addressString, System.Globalization.NumberStyles.HexNumber));
            mMemSharp.Assembly.Execute(addr, callingConvention, p);
        }

        private void AliveAPI_GameTick(object sender, EventArgs e)
        {
            if (bCallFunction)
            {
                try
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        string[] codeLines = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                        foreach(var s in codeLines)
                        {
                            DecodeInstructionLine(s);
                        }
                    }));
                }
                catch (Exception ex)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        label1.Text = "Error calling function!: " + ex.Message;
                    }));
                }

                bCallFunction = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bCallFunction = true;
        }

        private void FunctionCallerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void FunctionCallerWindow_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(Enum.GetNames(typeof(CallingConventions)));
            comboBox1.SelectedIndex = 0;
        }
    }
}
