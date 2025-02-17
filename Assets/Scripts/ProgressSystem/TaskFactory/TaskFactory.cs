﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class TaskFactory {
    /// <summary>
    /// Static class for creating a new task based on given type.
    /// </summary>
    /// <param name="type">Type given to turn into a Task.</param>
    /// <returns>Returns a new Task based on TaskType.</returns>
    public static ITask GetTask(TaskType type, SceneTypes scene) {
        if (scene == SceneTypes.MedicinePreparation)
        {
            switch (type)
            {
                case TaskType.SelectTools:
                    return new SelectTools();
                case TaskType.SelectMedicine:
                    return new SelectMedicine();
                case TaskType.CorrectItemsInThroughput:
                    return new CorrectItemsInThroughput();
                case TaskType.CorrectLayoutInThroughput:
                    return new CorrectLayoutInThroughput();
                case TaskType.CorrectItemsInLaminarCabinet:
                    return new CorrectItemsInLaminarCabinet();
                case TaskType.CorrectLayoutInLaminarCabinet:
                    return new CorrectLayoutInLaminarCabinet();
                case TaskType.DisinfectBottles:
                    return new DisinfectBottles();
                case TaskType.MedicineToSyringe:
                    return new MedicineToSyringe();
                case TaskType.LuerlockAttach:
                    return new LuerlockAttach();
                case TaskType.SyringeAttach:
                    return new SyringeAttach();
                case TaskType.CorrectAmountOfMedicineSelected:
                    return new CorrectAmountOfMedicineSelected();
                case TaskType.ItemsToSterileBag:
                    return new ItemsToSterileBag();
                case TaskType.ScenarioOneCleanUp:
                    return new ScenarioOneCleanUp();
                case TaskType.Finish:
                    return new Finish();
                default:
                    return null;
            }
        }
        if (scene == SceneTypes.MembraneFilteration)
        {
            switch (type)
            {
                case TaskType.CorrectItemsInThroughputMembrane:
                    return new CorrectItemsInThroughputMembrane();
                default:
                    return null;
            }
        }
        return null;
    }
}