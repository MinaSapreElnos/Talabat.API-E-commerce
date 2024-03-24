namespace Talabat.API.Errors
{
    public class APIRespons
    {
        public int StatsCode { get; set; }

        public string Massege { get; set; }

        public APIRespons( int _StatsCode , string? _Massege  =null )
        {
            StatsCode= _StatsCode ;

            Massege= _Massege ?? GetDefultMassegeForStatsCode(_StatsCode) ;
        }

        private string? GetDefultMassegeForStatsCode(int statsCode)
        {
            return statsCode switch
            {
                400 => "Bad Requst",
                401 => "Unauthorized, you are Not",
                404 => " Resourse is Not Found",
                500 => "Servar Error", 
                _ => null,  
            };
        }
    }
}
