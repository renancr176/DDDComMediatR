namespace ApiMvno.Infra.CrossCutting.Jsc.Models.Responses;

public class ConsumptionTotalsResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Cdrsummaryvoicemo CdrSummaryVoiceMo { get; set; }
    public Cdrsummaryvoicemt CdrSummaryVoiceMt { get; set; }
    public Cdrsummaryvideomo CdrSummaryVideoMo { get; set; }
    public Cdrsummaryvideomt CdrSummaryVideoMt { get; set; }
    public Cdrsummarysmsmo CdrSummarySmsmo { get; set; }
    public Cdrsummarysmsmt CdrSummarySmsmt { get; set; }
    public Cdrsummarymmsmo CdrSummaryMmsmo { get; set; }
    public Cdrsummarymmsmt CdrSummaryMmsmt { get; set; }
    public Cdrsummarydata CdrSummaryData { get; set; }
    public Cdrsummaryother CdrSummaryOther { get; set; }
    public float AmountChargedTotalMo { get; set; }
    public float AmountChargedTotalMt { get; set; }
    public float AmountChargedTotal { get; set; }
}

public class Cdrsummaryvoicemo
{
    public int Units { get; set; }
    public float AmountCharged { get; set; }
    public int TrafficUnits { get; set; }
    public float TrafficUnitsCost { get; set; }
    public float TrafficUnitsFree { get; set; }
    public float TrafficUnitsRated { get; set; }
    public float TrafficUnitsRatedCost { get; set; }
    public float TrafficUnitsRatedFree { get; set; }
}

public class Cdrsummaryvoicemt
{
    public int Units { get; set; }
    public float AmountCharged { get; set; }
    public int TrafficUnits { get; set; }
    public float TrafficUnitsCost { get; set; }
    public float TrafficUnitsFree { get; set; }
    public float TrafficUnitsRated { get; set; }
    public float TrafficUnitsRatedCost { get; set; }
    public float TrafficUnitsRatedFree { get; set; }
}

public class Cdrsummaryvideomo
{
    public int Units { get; set; }
    public float AmountCharged { get; set; }
    public int TrafficUnits { get; set; }
    public float TrafficUnitsCost { get; set; }
    public float TrafficUnitsFree { get; set; }
    public float TrafficUnitsRated { get; set; }
    public float TrafficUnitsRatedCost { get; set; }
    public float TrafficUnitsRatedFree { get; set; }
}

public class Cdrsummaryvideomt
{
    public int Units { get; set; }
    public float AmountCharged { get; set; }
    public int TrafficUnits { get; set; }
    public float TrafficUnitsCost { get; set; }
    public float TrafficUnitsFree { get; set; }
    public float TrafficUnitsRated { get; set; }
    public float TrafficUnitsRatedCost { get; set; }
    public float TrafficUnitsRatedFree { get; set; }
}

public class Cdrsummarysmsmo
{
    public int Units { get; set; }
    public float AmountCharged { get; set; }
    public int TrafficUnits { get; set; }
    public float TrafficUnitsCost { get; set; }
    public float TrafficUnitsFree { get; set; }
    public float TrafficUnitsRated { get; set; }
    public float TrafficUnitsRatedCost { get; set; }
    public float TrafficUnitsRatedFree { get; set; }
}

public class Cdrsummarysmsmt
{
    public int Units { get; set; }
    public float AmountCharged { get; set; }
    public int TrafficUnits { get; set; }
    public float TrafficUnitsCost { get; set; }
    public float TrafficUnitsFree { get; set; }
    public float TrafficUnitsRated { get; set; }
    public float TrafficUnitsRatedCost { get; set; }
    public float TrafficUnitsRatedFree { get; set; }
}

public class Cdrsummarymmsmo
{
    public int Units { get; set; }
    public float AmountCharged { get; set; }
    public int TrafficUnits { get; set; }
    public float TrafficUnitsCost { get; set; }
    public float TrafficUnitsFree { get; set; }
    public float TrafficUnitsRated { get; set; }
    public float TrafficUnitsRatedCost { get; set; }
    public float TrafficUnitsRatedFree { get; set; }
}

public class Cdrsummarymmsmt
{
    public int Units { get; set; }
    public float AmountCharged { get; set; }
    public int TrafficUnits { get; set; }
    public float TrafficUnitsCost { get; set; }
    public float TrafficUnitsFree { get; set; }
    public float TrafficUnitsRated { get; set; }
    public float TrafficUnitsRatedCost { get; set; }
    public float TrafficUnitsRatedFree { get; set; }
}

public class Cdrsummarydata
{
    public int Units { get; set; }
    public float AmountCharged { get; set; }
    public int TrafficUnits { get; set; }
    public float TrafficUnitsCost { get; set; }
    public float TrafficUnitsFree { get; set; }
    public float TrafficUnitsRated { get; set; }
    public float TrafficUnitsRatedCost { get; set; }
    public float TrafficUnitsRatedFree { get; set; }
}

public class Cdrsummaryother
{
    public int Units { get; set; }
    public float AmountCharged { get; set; }
    public int TrafficUnits { get; set; }
    public float TrafficUnitsCost { get; set; }
    public float TrafficUnitsFree { get; set; }
    public float TrafficUnitsRated { get; set; }
    public float TrafficUnitsRatedCost { get; set; }
    public float TrafficUnitsRatedFree { get; set; }
}