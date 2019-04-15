using AutoCarSim.Common;
using AutoCarSim.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCarSim.Models
{
    public class Simulation : BaseModel
    {
        private Map _map;
        private int _milisecondsPerSpawn;
        private int _amountOfThreads;
        private int _amountOfCars;
        //HEIGHT - at least 7!
        private int HEIGHT = 7;
        private int LENGTH = 20;
        private int MAIN_STEP_TIME = 1000;
        private int _steps = 0;
        private bool _running = false;

        public Map map
        {
            get { return _map; }

            set { this.SetProperty(ref this._map, value); }
        }

        public int milisecondsPerSpawn
        {
            get { return _milisecondsPerSpawn; }

            set { this.SetProperty(ref this._milisecondsPerSpawn, value); }
        }

        public int amountOfThreads
        {
            get { return _amountOfThreads; }

            set { this.SetProperty(ref this._amountOfThreads, value); }
        }

        public int amountOfCars
        {
            get { return _amountOfCars; }

            set { this.SetProperty(ref this._amountOfCars, value); }
        }

        public int steps
        {
            get { return _steps; }

            set { this.SetProperty(ref this._steps, value); }
        }

        public bool running
        {
            get { return _running; }

            set { this.SetProperty(ref this._running, value); }
        }

        public void start()
        {
            if (HEIGHT < 7) HEIGHT = 7;
            map.populateMap(HEIGHT, LENGTH, amountOfCars, amountOfThreads);
            running = true;
            //Main game loop Thread
            Task avehicleLoopTask = new Task(mainLoop, "AVehicle Loop");
            avehicleLoopTask.Start();
            //ThreadPool.QueueUserWorkItem(mainLoop);
            //Vehicle adding loop Thread
            //ThreadPool.QueueUserWorkItem(vehicleLoop);
            Task vehicleLoopTask = new Task(vehicleLoop, "Vehicle Loop");
            vehicleLoopTask.Start();
        }

        private void mainLoop(Object stateInfo)
        {
            bool crash = false;
            while (!crash)
            {
                map.makeStep();
                crash = map.checkCrash();
                Thread.Sleep(MAIN_STEP_TIME);
                steps += 1;
                Debug.WriteLine("Amount of steps: " + steps);
            }
            running = false;
            stop();
        }

        private void vehicleLoop(Object stateInfo)
        {
            while (running)
            {
                addVehicle();
                Thread.Sleep(milisecondsPerSpawn);
            }
        }

        public void addVehicle()
        {
            //TODO: Build a queue of last used lanes
            Random random = new Random();
            int randlane = random.Next(0, HEIGHT);
            if (randlane > HEIGHT - 2)
            {
                int rand = random.Next(0, 10);
                if(rand < 5)
                {
                    //Add truck
                    map.vehicles.Add(new Truck());
                    map.addNewVehicle(randlane, LENGTH);
                    Debug.WriteLine("Truck added! Lane: " + randlane);
                }
                else if (rand < 7.5) {
                    //Add bus
                    map.vehicles.Add(new Bus());
                    map.addNewVehicle(randlane, LENGTH);
                    Debug.WriteLine("Bus added! Lane: " + randlane);
                }
                else
                {
                    //Add car
                    map.vehicles.Add(new Car());
                    map.addNewVehicle(randlane, LENGTH);
                    Debug.WriteLine("Car added! Lane: " + randlane);
                }
            }
            else if (randlane > HEIGHT / 2)
            {
                int rand = random.Next(10);
                if (rand < 5)
                {
                    //Add bus
                    map.vehicles.Add(new Bus());
                    map.addNewVehicle(randlane, LENGTH);
                    Debug.WriteLine("Bus added! Lane: " + randlane);
                }
                else
                {
                    //Add car
                    map.vehicles.Add(new Car());
                    map.addNewVehicle(randlane, LENGTH);
                    Debug.WriteLine("Car added! Lane: " + randlane);
                }
            }
            else
            {
                //Add car
                map.vehicles.Add(new Car());
                map.addNewVehicle(randlane, LENGTH);
                Debug.WriteLine("Car added! Lane: " + randlane);
            }

        }

        public void stop()
        {
            //show crash or sth
        }
    }
}
