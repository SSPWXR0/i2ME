namespace MistWX_i2Me.Schema.ibm;

public class Almanac1DayResponse
{
    public List<string> almanacInterval { get; set; }
    public List<string> almanacRecordDate { get; set; }
    public List<string>? almanacRecordPeriod { get; set; }
    public List<string>? almanacRecordYearMax { get; set; }
    public List<string>? almanacRecordYearMin { get; set; }
    public List<string>? precipitationAverage { get; set; }
    public List<string>? snowAccumulationAverage { get; set; }
    public List<string> stationId { get; set; }
    public List<string>? stationName { get; set; }
    public List<string>? temperatureAverageMax { get; set; }
    public List<string>? temperatureAverageMin { get; set; }
    public List<string>? temperatureAverageMean { get; set; }
    public List<string>? temperatureRecordMax { get; set; }
    public List<string>? temperatureRecordMin { get; set; }
}