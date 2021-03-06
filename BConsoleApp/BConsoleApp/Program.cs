﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BConsoleApp
{
    class Program
    {
        private static Warehouse warehouse = new Warehouse();
        private static CancellationTokenSource flag = new CancellationTokenSource();
        static void Main(string[] args)
        {
            // Create a generic scanner
            IScanner reader = Scanner.Create(args[0]);
            //var ienumreader = reader.ReadItems();

            // Create ItemConsumer
            ItemConsumer consumer = new ItemConsumer();
            var task = consumer.ProcessAsync();

            int counter = 0;
            foreach (var item in reader.ReadItems())
            {
                var df = new DataFrame(item.PayLoad);
                consumer.Buffer.Add(new AsynchronousBufferItem() {
                    ID = string.Format("subframe_{0}",counter),
                    Words = df.Words
                });                
            }
            consumer.EndProcessing();

            task.Wait();

            Console.Out.WriteLine(string.Format("Consumer outputted subframe count of {0}", consumer.Elements.Count.ToString()));


            //Task loader = Program.LoadWareHouseTask();

            //Task unloader = Task.Factory.StartNew(() =>
            //{
            //    bool status = true;
            //    while (status)
            //    {
            //        Item item = warehouse.TakeItemFromInventory(out status, 500);
            //        Program.StateMachine(item);
            //        if (warehouse.EndProcessing)
            //            break;
            //    }
            //});

            Console.ReadKey();
            //Program.Done();
            //Task.WaitAll(loader, unloader);
            
        }

        public static Task LoadWareHouseTask()
        {
            return Task.Factory.StartNew(() =>
            {
                int idcounter = 0;
                bool done = false;
                while (!done)
                {
                    done = warehouse.AddItemToInventory(new Item { Id = idcounter++, Name = string.Format("Item_Name_{0}",idcounter) });
                    Thread.Sleep(100);
                }

            });
        }

        public static void Done()
        {
            warehouse.EndLoading();

        }

        public static void StateMachine(Item item)
        {
            if (item == null)
                return;

            switch (item.Id)
            {
                case 0:
                    Console.Out.WriteLineAsync(string.Format("Item Name: {0}", item.Name));
                    break;
                case 1:
                    Console.Out.WriteLineAsync(string.Format("Item Name: {0}", item.Name));
                    break;
                case 2:
                    Console.Out.WriteLineAsync(string.Format("Item Name: {0}", item.Name));
                    break;
                default:
                    Console.Out.WriteLineAsync(string.Format("Item Name: {0}", item.Name));
                    break;
            }
        }
    }
}
