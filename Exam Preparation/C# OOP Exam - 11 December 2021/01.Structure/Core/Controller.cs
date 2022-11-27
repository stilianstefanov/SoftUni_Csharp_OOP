namespace Gym.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Utilities.Messages;
    using Models.Equipment.Contracts;
    using Models.Gyms.Contracts;
    using Models.Gyms;
    using Models.Equipment;
    using Models.Athletes.Contracts;
    using Models.Athletes;
    using Repositories;

    public class Controller : IController
    {
        private EquipmentRepository equipment;
        private List<IGym> gyms;

        public Controller()
        {
            equipment = new EquipmentRepository();
            gyms = new List<IGym>();
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            if (athleteType != "Boxer" && athleteType != "Weightlifter")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }
            
            var gymToAddAthlete = gyms.FirstOrDefault(g => g.Name == gymName);

            IAthlete newAthlete= null;
            switch (athleteType)
            {
                case "Boxer":
                    {
                        newAthlete = new Boxer(athleteName, motivation, numberOfMedals);
                        if (gymToAddAthlete.GetType().Name != "BoxingGym")
                            return OutputMessages.InappropriateGym;

                        break;
                    }
                case "Weightlifter":
                    {
                        newAthlete = new Weightlifter(athleteName, motivation, numberOfMedals);
                        if (gymToAddAthlete.GetType().Name != "WeightliftingGym")
                            return OutputMessages.InappropriateGym;
                    }
                    break;                
            }

            gymToAddAthlete.AddAthlete(newAthlete);

            return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
        }

        public string AddEquipment(string equipmentType)
        {
            if (equipmentType != "BoxingGloves" && equipmentType != "Kettlebell")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }

            IEquipment newEquipment = null;
            switch (equipmentType)
            {
                case "BoxingGloves":
                    newEquipment = new BoxingGloves();
                    break;
                case "Kettlebell":
                    newEquipment = new Kettlebell();
                    break;                
            }

            equipment.Add(newEquipment);

            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string AddGym(string gymType, string gymName)
        {
            if (gymType != "BoxingGym" && gymType != "WeightliftingGym")
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }

            IGym newGym = null;
            switch (gymType)
            {
                case "BoxingGym":
                    newGym = new BoxingGym(gymName);
                    break;
                case "WeightliftingGym":
                    newGym = new WeightliftingGym(gymName);
                    break;                
            }

            gyms.Add(newGym);

            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string EquipmentWeight(string gymName)
        {
            var gym = gyms.FirstOrDefault(g => g.Name== gymName);

            return string.Format(OutputMessages.EquipmentTotalWeight, gymName, gym.EquipmentWeight);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            var equipmentToInsert = equipment.FindByType(equipmentType);
            if (equipmentToInsert == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }

            var gymToInsertAt = gyms.FirstOrDefault(g => g.Name== gymName);

            gymToInsertAt.AddEquipment(equipmentToInsert);
            equipment.Remove(equipmentToInsert);

            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string Report()
        {
            return string.Join(Environment.NewLine, gyms.Select(x => x.GymInfo()));
        }

        public string TrainAthletes(string gymName)
        {
            var gymToTrain = gyms.FirstOrDefault(g => g.Name == gymName);

            gymToTrain.Exercise();

            return string.Format(OutputMessages.AthleteExercise, gymToTrain.Athletes.Count);
        }
    }
}
