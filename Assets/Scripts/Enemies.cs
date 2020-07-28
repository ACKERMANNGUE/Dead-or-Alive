using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Enemies
{
    private const string PATH = @".\Assets\datas\enemies.csv";
    public Dictionary<string, int> dctEnemies;

    public Enemies()
    {
        dctEnemies = new Dictionary<string, int>();
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
                dctEnemies.Add(values[0], Convert.ToInt32(values[1]));
            }
        }
    }
}
