// using System;
// using Thrift;
// using Thrift.Protocol;
// using Thrift.Server;
// using Thrift.Transport;

// namespace osquery_csharp
// {
//     public class CSharpClient
//     {
//         public static void Main()
//         {
//             try
//             {
//                 TTransport transport = new TSocket("localhost", 9090);
//                 TProtocol protocol = new TBinaryProtocol(transport);
//                 Calculator.Client client = new Calculator.Client(protocol);

//                 transport.Open();
//                 try
//                 {
//                     client.ping();
//                     Console.WriteLine("ping()");

//                     int sum = client.add(1, 1);
//                     Console.WriteLine("1+1={0}", sum);

//                     Work work = new Work();

//                     work.Op = Operation.DIVIDE;
//                     work.Num1 = 1;
//                     work.Num2 = 0;
//                     try
//                     {
//                         int quotient = client.calculate(1, work);
//                         Console.WriteLine("Whoa we can divide by 0");
//                     }
//                     catch (InvalidOperation io)
//                     {
//                         Console.WriteLine("Invalid operation: " + io.Why);
//                     }

//                     work.Op = Operation.SUBTRACT;
//                     work.Num1 = 15;
//                     work.Num2 = 10;
//                     try
//                     {
//                         int diff = client.calculate(1, work);
//                         Console.WriteLine("15-10={0}", diff);
//                     }
//                     catch (InvalidOperation io)
//                     {
//                         Console.WriteLine("Invalid operation: " + io.Why);
//                     }

//                     SharedStruct log = client.getStruct(1);
//                     Console.WriteLine("Check log: {0}", log.Value);

//                 }
//                 finally
//                 {
//                     transport.Close();
//                 }
//             }
//             catch (TApplicationException x)
//             {
//                 Console.WriteLine(x.StackTrace);
//             }

//         }
//     }
// }