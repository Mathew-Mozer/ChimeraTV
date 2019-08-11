using System;
using System.Collections.Generic;
using System.Xml.Serialization;
[Serializable]
public class MonteCarloBoard
{
    public List<MonteCarloField> MonteCarloFields = new List<MonteCarloField>();
}