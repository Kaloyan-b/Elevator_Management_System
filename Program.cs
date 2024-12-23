using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;

namespace ElevatorManagement
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {

            List<Elevator> elevators = new List<Elevator>
            {
                new Elevator(0),
                new Elevator(1),
                new Elevator(2)
            };

            ConcurrentQueue<Request> requests = new ConcurrentQueue<Request>();
            _ = Task.Run(ProcessRequests);


            Instructions();
            UpdateStatusPanel();
            string[] cmd = Console.ReadLine().Split().ToArray();
            int countOfCalls = 0;
            while (cmd[0].ToLower() != "stop")
            {
                if (cmd[0].ToLower() == "move")
                {
                    int Selected = int.Parse(cmd[1]);
                    int floorSelected = int.Parse(cmd[2]);
                    await elevators[Selected].Move(floorSelected);
                    UpdateStatusPanel();
                }
                else if (cmd[0].ToLower() == "call")
                {
                    int floor = int.Parse(cmd[1]);
                    int floorSelected = int.Parse(cmd[2]);
                    await Call(floor, floorSelected);
                    countOfCalls++;
                    UpdateStatusPanel();
                }
                else if (cmd[0].ToLower() == "spread")
                {
                    await SpreadOut();
                    UpdateStatusPanel();
                }

                if(countOfCalls > 3)
                {
                    SpreadOut();
                    countOfCalls = 0;
                }

                cmd = Console.ReadLine().Split().ToArray();
            }

            // ---------------------- METHODS ----------------------
            async Task Call(int floor, int selected)
            {
                var request = new Request(floor, selected);
                requests.Enqueue(request);
                Console.WriteLine($"Request added: From Floor {floor} to Floor {selected}");
            }

            async Task ProcessRequests()
            {
                while (true)
                {
                    if(requests.TryDequeue(out var request))
                    {
                        Console.WriteLine($"Processing request from {request.SourceFloor} to {request.DestinationFloor}");

                        int bestCase = int.MaxValue;
                        int bestId = 0;
                        foreach (Elevator elevator in elevators)
                        {
                            if (elevator.Floor > request.SourceFloor)
                            {
                                if (bestCase > elevator.Floor - request.SourceFloor)
                                {
                                    bestCase = elevator.Floor - request.SourceFloor;
                                    bestId = elevator.Id;
                                }
                            }
                            else
                            {
                                if (bestCase > request.SourceFloor - elevator.Floor)
                                {
                                    bestCase = request.SourceFloor - elevator.Floor;
                                    bestId = elevator.Id;
                                }
                            }
                        }
                        if (elevators[bestId].Floor == request.SourceFloor)
                        {
                            Console.WriteLine($"Elevator({elevators[bestId].Id}) is already at floor: {request.SourceFloor}. And will take the call");
                            elevators[bestId].Move(request.DestinationFloor);
                            UpdateStatusPanel();
                        }
                        else
                        {
                            await elevators[bestId].Move(request.SourceFloor);
                            await elevators[bestId].Move(request.DestinationFloor);
                            UpdateStatusPanel();
                        }
                    }
                    else
                    {
                        await Task.Delay(250);
                    }
                }
            }



            async Task SpreadOut()
            {
                int maxFloor = 10;
                int spread = maxFloor / elevators.Count;
                for (int i = 0; i < elevators.Count; i++)
                {
                    int target = i * spread;
                    if (elevators[i].Floor != target)
                    {
                        Console.WriteLine($"Spreading out Elevator({elevators[i].Id}) to floor {target}");
                        await elevators[i].Move(target);
                    }
                }
                Console.WriteLine("Elevators have been spread out.");
                UpdateStatusPanel();
            }


            void Instructions()
            {
                Console.WriteLine("TO MOVE A CABIN, USE COMMAND: move {elevator id} {floor to move to}");
                Console.WriteLine("TO SIMULATE AN ELEVATOR CALL, USE COMMAND: call {floor calling from} {floor you want to go to}");
            }

            void UpdateStatusPanel()
            {
                for (int i = 0; i < elevators.Count; i++)
                {
                    elevators[i].UpdateStatusPanel();
                }
            }
            UpdateStatusPanel();
        }
    }
}
