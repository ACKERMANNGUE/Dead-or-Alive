using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Items
{
    private const string PATH = @"F:\Personnel\Procedural levels\Assets\datas\items.csv";
    public Dictionary<string, string> dctItems;

    public Items()
    {
        dctItems = new Dictionary<string, string>();
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
                dctItems.Add(values[0], values[1]);
            }
        }
    }
}
