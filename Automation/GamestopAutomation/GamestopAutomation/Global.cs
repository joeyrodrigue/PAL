/*
 * Created by Ranorex
 * User: storeuser
 * Date: 7/15/2013
 * Time: 12:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Collections.Generic;
using GSLogger;
using System.Xml.Linq;

namespace GamestopAutomation
{
	/// <summary>
	/// Description of Global.
	/// </summary>
	public class Global
	{
		public static int		intTestID,
    							intIteration;
    							
    	public static string	strScenerio,
    							strScCheckPoint,
    							strRegister,
    							strID,
    							CriteriaType;
    	
    	public static string[]	arrMetricData,
    							arrSteps;
    	
    	public static XElement	xelModule;
    	
    	public static IEnumerable<XElement>	Criteria;
    	
    	public static XDocument xdocModule,
    							xdocScenario;
    	
					
   							
    	public static TimeSpan 	tsDuration;
    	
    	public static DateTime	dtEventTime;
    	
    	public static Stopwatch stwIterationStopWatch,
    							stwStepStopWatch;
    	
    	public static LogFile logger = new LogFile();
    	
    	public static Boolean 	IsPerformanceTest,
    							Proceed = true;
    	
    	public static string strRegisterIni = "c:\\pos\\register.ini";
    	
    	   	
    	
		public Global()
		{

			
		}

	}
}
