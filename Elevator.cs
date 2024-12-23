using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorManagement
{
    public class Elevator
    {
        private int _floor;
        private bool _stopped;
        private bool _called;
        private int _maxFloors; // TODO
        private int _id;

        public int Floor 
        {
            get { return _floor; }
            set {  _floor = value; } 
        }
        public bool Stopped
        {
            get { return _stopped; }
            set { _stopped = value; }
        }
        public bool Called
        {
            get { return _called; }
            set { _called = value; }
        }
        public int Id 
        { 
            get { return _id; }
            set { _id = value; } 
        }

        public Elevator(int id)
        {
            _floor = 0;
            _stopped = true;
            _called = false;
            _id = id;
        }
        Random rnd = new Random();
        public async Task Move(int floor)
        {
            if (this.Stopped) 
            {
                this.Stopped = false;
                if (_floor < floor)
                {
                    for (int i = _floor; i <= floor; i++)
                    {
                        Console.WriteLine($"Elevator({this.Id}) is at floor {i}, moving up to {floor} floor...");
                        await Task.Delay(rnd.Next(1000, 2000)); // Delay
                        this._floor = i;
                        Console.Clear();
                    }
                    this.Stopped = true;
                    Console.WriteLine($"Elevator({this.Id}) is at floor {this.Floor}");
                }
                else if (_floor > floor)
                {
                    for (int i = _floor; i >= floor; i--)
                    {
                        Console.WriteLine($"Elevator({this.Id}) is at floor {i}, moving down to {floor} floor...");
                        await Task.Delay(rnd.Next(1000, 2000)); // Delay
                        this._floor = i;
                        Console.Clear();
                    }
                    this.Stopped = true;
                    Console.WriteLine($"Elevator({this.Id}) is at floor {this.Floor}");
                }
            }
            else
            {
                Console.WriteLine($"Elevator({this.Id}) is currently respoding to a call");
            }
        }
        public void UpdateStatusPanel()
        {

            
            Console.WriteLine($"Elevator {this.Id}: Floor = {this.Floor}, Stopped = {this.Stopped}".PadRight(Console.WindowWidth));
            

            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop); // Restore cursor position
        }
    }
}
