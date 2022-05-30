using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryScript
{
    string name;
    string code;
    int yesterdayTotalCases;
    int todayTotalCases;
    int newCases;

    public CountryScript()
    {
    }

    public CountryScript(string name, string code, int yesterdayTotalCases, int todayTotalCases, int newCases)
    {
        this.name = name;
        this.code = code;
        this.yesterdayTotalCases = yesterdayTotalCases;
        this.todayTotalCases = todayTotalCases;
        this.newCases = newCases;
    }

    public string PrintAllData()
    {
        string allData = "Country - " + name + "\nCode - " + code + "\nYesterday Total Cases - " + yesterdayTotalCases + "\nToday Total Cases - " + todayTotalCases + "\nNew Cases - " + newCases;
        return allData;
    }

    public string GetCode() { return code; }
}
