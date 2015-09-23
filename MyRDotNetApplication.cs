using RDotNet;

namespace MyRDotNetApplication
{
	class Program
	{
	        public static int prevFileLastWiredTimestamp = 0;
	        public static int prevFileLastWifiTimestamp = 0;        
	 
	        static string[] files;
	 
	        static void Main(string[] args)
	        {
		    	string rawLatencyOutput = @"C:\\latencyOutput.csv";
		
		    	REngine.SetEnvironmentVariables();
		    	REngine engine = REngine.GetInstance();
		    	engine.Initialize();
		
		    	GenetateLatencyVsMessageLengthPlot_RScript(engine, rawLatencyOutput);
		
		    	engine.Dispose();
	    	}
			
		static void GenetateLatencyVsMessageLengthPlot_RScript(REngine engine, string latencyCsvFilename)
	        {
	            ////////////////////////////////////////////////////////
	            //  R-Script to generate plot            
	            string fileToReadFromCommand = latencyCsvFilename.Replace(@"\", @"/");
	
	            CharacterVector cvFilename = engine.CreateCharacterVector(new[] { fileToReadFromCommand });
	
	            engine.SetSymbol("fileToReadFrom", cvFilename);
	
	            engine.Evaluate(MyRDotNetApplication.Properties.Resources.latencyVsMessageLengthScatterPlot); //script is a string resource
	            //////////////////////////////////////////////////////            
	        }
	}	
}

