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
        private int _amountOfThreads; //store amount of threads to be used
        private int _amountOfCars;
        //HEIGHT - at least 7!
        private int HEIGHT = 7; // max x
        private int LENGTH = 20;// max y
        private int MAIN_STEP_TIME = 1000; //time it takes for the map to refresh
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

            //start main loop of the map
            Task avehicleLoopTask = new Task(mainLoop, "AVehicle Loop");
            avehicleLoopTask.Start();

            //start secondary loop of adding enemy vehicles continuously
            Task vehicleLoopTask = new Task(vehicleLoop, "Vehicle Loop");
            vehicleLoopTask.Start();
        }

        private void mainLoop(Object stateInfo)
        {
            bool crash = false;
            while (!crash)
            {
                map.makeStepAsync();
                crash = map.checkCrash();
                Thread.Sleep(MAIN_STEP_TIME);
                steps += 1;
                Debug.WriteLine("Amount of steps: " + steps);
            }
            running = false;
            stopAsync();
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
            Random random = new Random();
            int randlane = random.Next(0, HEIGHT);

            int rand = random.Next(0, 10);
            if(rand < 2)
            {
                //Add truck
                map.vehicles.Add(new Truck());
                map.addNewVehicle(randlane, LENGTH);
                Debug.WriteLine("Truck added! Lane: " + randlane);
            }
            else if (rand < 5) {
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

        //show car crash message dialog
        public async void stopAsync()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            async () =>
            {
                await new Windows.UI.Popups.MessageDialog("CRASH!!!").ShowAsync();
            });
        }
    }
}
