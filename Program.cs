namespace UMICH_Problem_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    // Class to represent ETCO2 measurement that has two properties: ETCO2 measurement and time of measurement
    public class ETCO2
    {
        public double ETCO2_Value { get; set; }
        public DateTime TimeOfMeasurement { get; set; }

        public ETCO2(double ETCO2_Value, DateTime TimeOfMeasurement)
        {
            this.ETCO2_Value = ETCO2_Value;
            this.TimeOfMeasurement = TimeOfMeasurement;
        }
    }

    internal class MalignantHyperthermia
    {

        // Create a list to store the ETCO2 measurement
        List<ETCO2> ETCO2_Measurements= new List<ETCO2>();

        public void ProcessETCO2(ETCO2 measurement, DateTime time_of_measurement)
        {
            // Add ETCO2 arguments to list of ETCO2 measurements
            ETCO2_Measurements.Add(measurement);

            // initialize max ETCO2 to first index in list
            ETCO2 maxETCO2 = ETCO2_Measurements[0];

            // Loop through list and check if there is a value greater than what we have the max value saved as
            // If there is, then set that as the max value.
            foreach (ETCO2 m in ETCO2_Measurements)
            {
                if (m.ETCO2_Value > maxETCO2.ETCO2_Value)
                {
                    maxETCO2 = m;
                }
            }

            // Calculate the slope and r^2 of the line through the 10 data points
            if (ETCO2_Measurements.Count > 10)
            {
                ETCO2_Measurements.RemoveAt(0);
            }
            else
            {
                double sumX = 0, sumY = 0, sumXY = 0, sumX2 = 0, sumY2 = 0;
                for (int i = 0; i < 10; i++)
                {
                    sumX += ETCO2_Measurements[i].TimeOfMeasurement.Minute;
                    sumY += ETCO2_Measurements[i].ETCO2_Value;
                    sumXY += ETCO2_Measurements[i].TimeOfMeasurement.Minute * ETCO2_Measurements[i].ETCO2_Value;
                    sumX2 += ETCO2_Measurements[i].TimeOfMeasurement.Minute * ETCO2_Measurements[i].TimeOfMeasurement.Minute;
                    sumY2 += ETCO2_Measurements[i].ETCO2_Value * ETCO2_Measurements[i].ETCO2_Value;
                }

                double r2 = ((10*sumXY) - (sumX*sumY)) / Math.Sqrt((10 * sumX2 - sumX * sumX) * (10 * sumY2 - sumY * sumY));

                double slope = ((10 * sumXY) - (sumX * sumY)) / ((10 * sumX2) - (sumX * sumX));

                if (slope > 1.5 && r2 > 0.64)
                {
                    Alarm();
                }
            }

        }

        public void Alarm()
        {
            Console.WriteLine("Malignant hyperthermia detected!");
        }

    }
}