using RDotNet;

namespace MyRDotNetApplication
{
	class Program
	{
	        static void Main(string[] args)
	        {
	        	//Assume the file exists
		    	string rawLatencyOutput = @"C:\\latencyOutput.csv";
		
			//Setup and init a new R-engine
		    	REngine.SetEnvironmentVariables();
		    	REngine engine = REngine.GetInstance();
		    	engine.Initialize();
		
			//Call the function to evaluate the script
		    	GenetateLatencyVsMessageLengthPlot_RScript(engine, rawLatencyOutput);
		
			//Stop the R-engine when done
		    	engine.Dispose();
	    	}
			
		static void GenetateLatencyVsMessageLengthPlot_RScript(REngine engine, string latencyCsvFilename)
	        {
	        	//For formatting purposes, make sure the filename is acceptable for R function read.csv
			string fileToReadFromCommand = latencyCsvFilename.Replace(@"\", @"/");
			
			//Convert to R character vector
			CharacterVector cvFilename = engine.CreateCharacterVector(new[] { fileToReadFromCommand });
			// and assign it to variable (in R engine) called fileToReadFrom
			engine.SetSymbol("fileToReadFrom", cvFilename);
			
			//And then evaluate the script - this uses the 'fileToReadFrom' in a read.csv call
			engine.Evaluate(MyRDotNetApplication.Properties.Resources.latencyVsMessageLengthScatterPlot); //R-Script to generate plot  
	        }
	}	
}

