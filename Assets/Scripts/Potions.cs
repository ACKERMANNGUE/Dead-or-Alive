using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Potions
{
    private const string PATH = @"F:\Personnel\Procedural levels\Assets\datas\potions.csv";
    public Dictionary<string, int> dctPotions;

    public Potions()
    {
        dctPotions = new Dictionary<string, int>();
        LoadDictionnary();
    }

    private void LoadDictionnary()
    {
        using (var reader = new StreamReader(PATH))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                //Debug.Log(string.Format("{0} - {1}", values[0], values[1]));
                dctPotions.Add(values[0], Convert.ToInt32(values[1]));
            }
        }
    }
}
