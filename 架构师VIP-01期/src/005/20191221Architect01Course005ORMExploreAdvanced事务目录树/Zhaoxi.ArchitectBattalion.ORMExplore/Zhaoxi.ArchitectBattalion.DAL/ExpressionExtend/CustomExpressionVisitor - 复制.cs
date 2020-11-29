//using System;
//using System.Collections.Generic;
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

//        public string GetWhere()
//        {
//            string where = string.Concat(this.ConditionStack.ToArray());
//            this.ConditionStack.Clear();
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
//            base.Visit(node.Right);
//            this.ConditionStack.Push(node.NodeType.ToSqlOperator());
//            base.Visit(node.Left);
//            this.ConditionStack.Push(" ( ");
//            return node;
//        }

//        protected override Expression VisitConstant(ConstantExpression node)
//        {
//            Console.WriteLine($"VisitConstant：{node.NodeType} {node.Type} {node.ToString()}");
//            this.ConditionStack.Push($"'{node.Value.ToString()}'");
//            return node;
//        }

//        protected override Expression VisitMember(MemberExpression node)
//        {
//            Console.WriteLine($"VisitMember：{node.NodeType} {node.Type} {node.ToString()}");
//            //this.ConditionStack.Push($"{node.Member.GetMappingName()}");
//            if (node.Expression is ConstantExpression)
//            {
//                var value1 = this.InvokeValue(node);
//                var value2 = this.ReflectionValue(node);
//                this.ConditionStack.Push($"'{value1}'");
//            }
//            else
//            {
//                //this.ConditionStack.Push($"{node.Member.Name}");
//                this.ConditionStack.Push($"{node.Member.GetMappingName()}");//映射数据
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
//            this.Visit(m.Object);
//            this.Visit(m.Arguments[0]);
//            string right = this.ConditionStack.Pop();
//            string left = this.ConditionStack.Pop();
//            this.ConditionStack.Push(String.Format(format, left, right));

//            return m;
//        }
//    }
//}
