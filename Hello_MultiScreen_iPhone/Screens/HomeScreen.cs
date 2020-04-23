using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace Hello_MultiScreen_iPhone
{
	public partial class HomeScreen : UIViewController
	{
		HelloWorldScreen helloWorldScreen;
		HelloUniverseScreen helloUniverseScreen;

        public enum OperationType { Add, Deduct, Mult, Div, None, Error }

        public class CalcTransaction
        {
            public decimal A { get; private set; }
            public decimal B { get; private set; }
            public decimal Result { get; private set; }
            public OperationType Operation { get; private set; }

            CalcTransaction()
            {
                A = B = Result = 0;
                Operation = OperationType.None;
            }

            CalcTransaction(decimal a, decimal b, OperationType op)
            {
                ExecuteOperation(a, b, op);
            }
            /// <summary>
            /// Main execute operation for single transaction
            /// </summary>
            /// <param name="a">first operator</param>
            /// <param name="b">second operator</param>
            /// <param name="op">operation  that will be performed</param>
            /// <returns></returns>
            decimal ExecuteOperation(decimal a, decimal b, OperationType op)
            {
                try
                {
                    A = a;
                    B = b;
                    Operation = op;
                    switch (op)
                    {
                        case OperationType.Add:
                            Result = A + B;
                            break;
                        case OperationType.Deduct:
                            Result = A - B;
                            break;
                        case OperationType.Mult:
                            Result = A * B;
                            break;
                        case OperationType.Div:
                            Result = A / B;
                            break;
                        default:
                            Result = 0;
                            break;
                    }
                }
                catch (Exception e)
                {
                    // TODO: Log error here!!!
                    Operation = OperationType.Error;
                    Result = 0;
                }
                return Result;
            }
        }

        List<CalcTransaction> _transactions = new List<CalcTransaction>();

        string _txtCurrentNumber;
        decimal _dSavedNumber = 0;
        bool _bTextReset = false;

        //loads the HomeScreen.xib file and connects it to this object
        public HomeScreen () : base ("HomeScreen", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            txtNumber.Text = "";
			//---- when the hello world button is clicked
			this.btnHelloWorld.TouchUpInside += (sender, e) => {
				//---- instantiate a new hello world screen, if it's null (it may not be null if they've navigated
				// backwards from it
				if(this.helloWorldScreen == null) { this.helloWorldScreen = new HelloWorldScreen(); }
				//---- push our hello world screen onto the navigation controller and pass a true so it navigates
				this.NavigationController.PushViewController(this.helloWorldScreen, true);
			};

			//---- same thing, but for the hello universe screen
			this.btnHelloUniverse.TouchUpInside += (sender, e) => {
				if(this.helloUniverseScreen == null) { this.helloUniverseScreen = new HelloUniverseScreen(); }
				this.NavigationController.PushViewController(this.helloUniverseScreen, true);
			};
		}

		/// <summary>
		/// Is called when the view is about to appear on the screen. We use this method to hide the
		/// navigation bar.
		/// </summary>
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, animated);
		}

		/// <summary>
		/// Is called when the another view will appear and this one will be hidden. We use this method
		/// to show the navigation bar again.
		/// </summary>
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			this.NavigationController.SetNavigationBarHidden (false, animated);
		}

        //public class dCalculus
        //{
        //    public enum Operation { Addition, Substraction, Multiplication, Division };

        //    public interface IOperandBase : ICloneable
        //    {
        //        static int MaxRankPerBucket { get; }
        //        object GetValue();
        //        static KeyValuePair<int, bool> GetRank(Int64 nValue);
        //        IOperandBase Execute(Operation op, IOperandBase a, IOperandBase b);

        //    }

        //    public class IntegerOperand : IOperandBase
        //    {
        //        List<KeyValuePair<Int64, int>> _NumberBucketList = new List<KeyValuePair<Int64, int>>();
        //        Int64 _nRank = 0;
        //        static int _maxRankPerBucket = 0;
        //        bool _bPositive = true;

        //        static int MaxRankPerBucket
        //        {
        //            get
        //            {
        //                if (_maxRankPerBucket == 0)
        //                {
        //                    _maxRankPerBucket = GetRank(Int64.MaxValue).Key - 1;
        //                }
        //                return _maxRankPerBucket;
        //            }
        //        }


        //        static KeyValuePair<int, bool> GetRank(Int64 nValue)
        //        {
        //            int rank = 0;

        //            bool positive = nValue > 0;
        //            Int64 tmp = (positive) ? nValue : -nValue;

        //            while (tmp > 0)
        //            {
        //                ++rank;
        //                tmp = tmp >> 1;
        //            }
        //            return new KeyValuePair<int, bool>(rank, positive);
        //        }

        //        IntegerOperand()
        //        {
        //            _NumberBucketList.Add(new KeyValuePair<Int64, int>(0, 0));

        //        }

        //        IntegerOperand(Int64 nNumber)
        //        {
        //            KeyValuePair<int, bool> rank = GetRank(nNumber);
        //            _bPositive = rank.Value;
        //            _nRank = rank.Key;
        //            _NumberBucketList.Add(new KeyValuePair<Int64, int>(nNumber, rank.Key));
        //        }

        //        public IOperandBase Execute(Operation op, IOperandBase a, IOperandBase b)
        //        {
        //            switch (op)
        //            {
        //                case Operation.Addition:
        //                    break;
        //                case Operation.Division:
        //                    break;
        //                case Operation.Substraction:
        //                    break;
        //                case Operation.Multiplication:
        //                    break;
        //            }
        //            return this;
        //        }

        //        public object GetValue()
        //        {
        //            throw new NotImplementedException();
        //        }

        //        public object Clone()
        //        {
        //            throw new NotImplementedException();
        //        }

        //    }

        //    public class DoubleOperand : IOperandBase
        //    {
        //        double _operand;

        //        public DoubleOperand()
        //        {
        //            _operand = 0.0;
        //        }

        //        public DoubleOperand(double value)
        //        {
        //            _operand = value;
        //        }

        //        public static DoubleOperand operator +(DoubleOperand recepient, IOperandBase sender)
        //        {

        //            return new DoubleOperand();
        //        }

        //        public object GetValue()
        //        {
        //            return _operand;
        //        }

        //        public int GetRank10()
        //        {
        //            throw new NotImplementedException();
        //        }

        //        public IOperandBase Execute(Operation op, IOperandBase a, IOperandBase b)
        //        {
        //            throw new NotImplementedException();
        //        }

        //        public object Clone()
        //        {
        //            throw new NotImplementedException();
        //        }
        //    }

        //    public class OperationsBase
        //    {
        //        protected IOperandBase _operand1;
        //        protected IOperandBase _operand2;
        //        protected IOperandBase _result;

        //        static List<OperationsBase> _operations = new List<OperationsBase>();
        //        string _strName = "Default Zero Operation";

        //        public static OperationsBase[] GetOperationsArray() { return _operations.ToArray(); }

        //        public string Name { get { return _strName; } }

        //        public OperationsBase(string operationName) { _strName = operationName; }

        //        public virtual IOperandBase Execute(Operation op, IOperandBase a, IOperandBase b)
        //        {
        //            _operand1 = a;
        //            _operand2 = b;
        //            _result = a.Execute(op, a, b);
        //            _operations.Add(this);
        //            return _result;
        //        }
        //    }

        //    public class AddOperation : OperationsBase
        //    {
        //        public AddOperation() : base("Addition operation") { }

        //        public override IOperandBase Execute(Operation op, IOperandBase a, IOperandBase b)
        //        {

        //            return base.Execute(op, a, b);
        //        }
        //    }

        //    public class DivideIntegerOperation : OperationsBase
        //    {
        //        public DivideIntegerOperation() : base("Divide integer operation") { }

        //        public override IOperandBase Execute(Operation op, IOperandBase a, IOperandBase b)
        //        {
        //            var d1rank = a.GetRank10();
        //            var d2rank = b.GetRank10();

        //            if (d2rank > d1rank)
        //            {
        //                return new DoubleOperand(0.0);
        //            }
        //            else
        //            {

        //            }
        //            return base.Execute(op, a, b);
        //        }
        //    }
        //}

        partial void Num1_TouchUpInside(UIButton sender)
        {
			txtNumber.Text += "1";
        }

        partial void Num2_TouchUpInside(UIButton sender)
        {
            txtNumber.Text += "2";
        }

        partial void Num3_TouchUpInside(UIButton sender)
        {
            txtNumber.Text += "3";
        }

        partial void Num4_TouchUpInside(UIButton sender)
        {
            txtNumber.Text += "4";
        }

        partial void Num5_TouchUpInside(UIButton sender)
        {
            txtNumber.Text += "5";
        }

        partial void Num6_TouchUpInside(UIButton sender)
        {
            txtNumber.Text += "6";
        }

        partial void Num7_TouchUpInside(UIButton sender)
        {
            txtNumber.Text += "7";
        }

        partial void Num8_TouchUpInside(UIButton sender)
        {
            txtNumber.Text += "8";
        }

        partial void Num9_TouchUpInside(UIButton sender)
        {
            txtNumber.Text += "9";
        }

        partial void Num0_TouchUpInside(UIButton sender)
        {
            txtNumber.Text += "0";
        }

        partial void BtnAdd_TouchUpInside(UIButton sender)
        {
            if (_dSavedNumber == 0)
                _dSavedNumber = Convert.ToDecimal(txtNumber.Text);
            else
            {
                _dSavedNumber += Convert.ToDecimal(txtNumber.Text);
                txtNumber.Text = _dSavedNumber.ToString();
            }
        }

        partial void BtnDeduct_TouchUpInside(UIButton sender)
        {
            if (_dSavedNumber == 0)
                _dSavedNumber = Convert.ToDecimal(txtNumber.Text);
            else
            {
                _dSavedNumber -= Convert.ToDecimal(txtNumber.Text);
                txtNumber.Text = _dSavedNumber.ToString();
            }
        }

        partial void BtnMultiply_TouchUpInside(UIButton sender)
        {
            if (_dSavedNumber == 0)
                _dSavedNumber = Convert.ToDecimal(txtNumber.Text);
            else
            {
                _dSavedNumber *= Convert.ToDecimal(txtNumber.Text);
                txtNumber.Text = _dSavedNumber.ToString();
            }
        }

        partial void BtnDivide_TouchUpInside(UIButton sender)
        {
            if (_dSavedNumber == 0)
                _dSavedNumber = Convert.ToDecimal(txtNumber.Text);
            else
            {
                _dSavedNumber /= Convert.ToDecimal(txtNumber.Text);
                txtNumber.Text = _dSavedNumber.ToString();
            }
        }

        partial void BtnResult_TouchUpInside(UIButton sender)
        {
        }

        partial void Negate_TouchUpInside(UIButton sender)
        {
            throw new NotImplementedException();
        }

        partial void BtnReset_TouchUpInside(UIButton sender)
        {
            txtNumber.Text = "";
        }
    }
}
