using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatiGiornalieri
{
    string name;
    string code;
    int dailyCases;

    public DatiGiornalieri(string name)
    {
        this.name = name;
    }

    public void SetCode(string code)
    {
        if (code == null)
            return;
        this.code = code;
    }
    public void SetDailyCases(int dailyCases)
    {
        this.dailyCases = dailyCases;
    }

    public string GetAllDataString()
    {
        string allDataString = "Country-" + name + "\nCode-" + code + "\nCases-" + dailyCases;
        return allDataString;
    }

    public string GetName()
    {
        return name;
    }

    public string GetCode()
    {
        return code;
    }
    public int GetDailyCases()
    {
        return dailyCases;
    }
}
