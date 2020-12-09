//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using Zhaoxi.ArchitectBattalion.Framework.SqlMapping;

//namespace Zhaoxi.ArchitectBattalion.DAL.ExpressionExtend
//{
//    public class CustomExpressionVisitor : ExpressionVisitor
//    {
//        private Stack<string> ConditionStack = new Stack<string>();
//        private List<SqlParameter> _ParameterList = new List<SqlParameter>();
//        private object _TempValue = null;


//        public string GetWhere(out List<SqlParameter> sqlParameters)
//        {
//            string where = string.Concat(this.ConditionStack.ToArray());
//            this.ConditionStack.Clear();
//            sqlParameters = _ParameterList;
//            return where;
//        }


//        public override Expression Visit(Expression node)
//        {
//            Console.WriteLine($"Visit入口：{node.NodeType} {node.Type} {node.ToString()}");
//            return base.Visit(node);
//        }

//        protected override Expression VisitBinary(BinaryExpression node)
//        {
//            Console.WriteLine($"VisitBinary：{node.NodeType} {node.Type} {node.ToString()}");
//            this.ConditionStack.Push(" ) ");
//            base.Visit(node.Right);//5
//            this.ConditionStack.Push(node.NodeType.ToSqlOperator()); //翻译成>
//            base.Visit(node.Left);//Age
//            this.ConditionStack.Push(" ( ");
//            return node;
//        }

//        protected override Expression VisitConstant(ConstantExpression node)
//        {
//            Console.WriteLine($"VisitConstant：{node.NodeType} {node.Type} {node.ToString()}");
//            //node.Value;
//            this._TempValue = node.Value;//不放入栈，放在临时变量
//            //this.ConditionStack.Push($"'{this._TempValue.ToString()}'");
//            return node;
//        }

//        protected override Expression VisitMember(MemberExpression node)
//        {
//            Console.WriteLine($"VisitMember：{node.NodeType} {node.Type} {node.ToString()}");
//            if (node.Expression is ConstantExpression)
//            {
//                var value1 = this.InvokeValue(node);
//                var value2 = this.ReflectionValue(node);
//                //this.ConditionStack.Push($"'{value1}'");
//                this._TempValue = value1;

//                //var operatorString = this.ConditionStack.Pop();
//                //var propString = this.ConditionStack.Peek();
//                //this.ConditionStack.Push(operatorString);
//                //this.ConditionStack.Push($"@{propString}");
//                //this._ParameterList.Add(new SqlParameter($"@{propString}", value1));
//            }
//            else
//            {
//                string name = node.Member.GetMappingName();

//                //this.ConditionStack.Push($"{node.Member.Name}");
//                if (this._TempValue != null)
//                {
//                    var operatorString = this.ConditionStack.Pop();

//                    string paraName = $"@{name}{this._ParameterList.Count()}";
//                    this.ConditionStack.Push(paraName);
//                    this.ConditionStack.Push(operatorString);
//                    this.ConditionStack.Push($"{name}");

//                    object value = this._TempValue;
//                    this._TempValue = null;

//                    this._ParameterList.Add(new SqlParameter(paraName, value));
//                }
//            }
//            return node;
//        }

//        private object InvokeValue(MemberExpression member)
//        {
//            var objExp = Expression.Convert(member, typeof(object));//struct需要
//            return Expression.Lambda<Func<object>>(objExp).Compile().Invoke();
//        }

//        private object ReflectionValue(MemberExpression member)
//        {
//            var obj = (member.Expression as ConstantExpression).Value;
//            return (member.Member as FieldInfo).GetValue(obj);
//        }

//        protected override Expression VisitMethodCall(MethodCallExpression m)
//        {
//            if (m == null) throw new ArgumentNullException("MethodCallExpression");

//            this.Visit(m.Arguments[0]);
//            string format;
//            switch (m.Method.Name)
//            {
//                case "StartsWith":
//                    format = "({0} LIKE {1}+'%')";
//                    break;

//                case "Contains":
//                    format = "({0} LIKE '%'+{1}+'%')";
//                    break;

//                case "EndsWith":
//                    format = "({0} LIKE '%'+{1})";
//                    break;

//                default:
//                    throw new NotSupportedException(m.NodeType + " is not supported!");
//            }
//            this.ConditionStack.Push(format);
//            this.Visit(m.Object);

//            string left = this.ConditionStack.Pop();
//            format = this.ConditionStack.Pop();
//            string right = this.ConditionStack.Pop();//保证顺序
//            this.ConditionStack.Push(string.Format(format, left, right));

//            return m;
//        }
//    }
//}
