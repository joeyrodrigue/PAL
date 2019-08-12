/*
 * Created by Ranorex
 * User: StoreUser
 * Date: 04/12/13
 * Time: 11:07 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Alpha
{
    /// <summary>
    /// Description of RanorexAutomation.
    /// </summary>
    [TestModule("0605524F-5462-494A-97C8-BF348FD85810", ModuleType.UserCode, 1)]
    public class RanorexAutomation : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public RanorexAutomation()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
			Mouse.DefaultMoveTime = 0;
            Keyboard.DefaultKeyPressTime = 20;
            Delay.SpeedFactor = 0.0;
        }
        

    }

   
    // Used to accumulate time spent in error popup handling then time taken away from metric time 
    static class GlobalOverhead
     {
        internal static System.Diagnostics.Stopwatch Stopwatch = new System.Diagnostics.Stopwatch();
     }
   
    // Global variable use for Scenarios
    public class Global
    {
    	
    	// Production Issue Functions
    	public static string	SpecialCustomerPhoneNumber;
    	public static int 		NumberSpecialSKUs;
    	public static string[]	SpecialSKUs = new string[10];	
    	public static int 		NumberSpecialTrades;
    	public static string[]	SpecialTrades = new string[10]; 
    	public static string 	SpecialPayThisAmount;
    	public static bool 		SpecialCheckingOut = false;
    	public static string 	SpecialInitialDeposit;
		public static string	MiniDumpDirectory = @"C:\POS\MiniDump\";  
		public static bool 		SpecialReturningSKUs = false;
    	// Core variables
    	public static bool 		DoRegisterSoundAlerts = false;
    	public static bool 		RegisterRunningRetech = false;   
    	public static bool 		RegisterRunningIPOS = false;   
    	public static bool 		CombinedIPOS = false;      	
    	public static bool 		JustScenario32 = false;   
    	public static bool 		AbortScenario = false;  
    	public static bool		DomesticRegister = true;
    	public static string	SQLCommand = "";
    	public static bool 		ScenarioExecuted = false; 
    	
    	public const string		SkippedText = "";
    	public const int		MaxScenarioNumber = 50;
    	public const int		MaxSearchTimeout = 150;

    	public const int		RetryWaitSeconds = 5;
    	public const int		MaxRetries = 5;
    	public const int		BrowserBlankPageDelay = 20;    	
		public const bool 		OpenFileForOutput = false;
		public const bool 		OpenFileForAppend = true;
    	public const string		StatsFileDirectory = @"PAL\Performance";
    	public const string		ReportsFileDirectory = @"PAL\Reports";    	
    	public const string		AutomationFileDirectory = @"PAL\Ranorex Automation";
    	public const string     RegisterIni = "c:\\pos\\register.ini";
    	
    	public static bool		AutoRun = false;    	
    	public static string	CommandLineArg0;
    	public static string	CommandLineArg1;
	   	public static string	CommandLineArg2;  
	   	public static string	CommandLineArg3;     	   	
		
    	public static bool 		Windows10 = false;
	   	public static string	ProcessName;
	   	public static int		RetechScenariosPerformed;
    	public static int		UsePhoneNumberType;
    	public static int		PhoneNumberOffset;
    	public static int		CurrentScenario;
    	public static int 		CurrentIteration;
    	public static string	IterationsText; 
    	public static string	Module;    	
    	public static string	CurrentSKU;
    	public static string	CurrentSKUOverideValue;
		public static bool 		CurrentSKUOveride = false;       
		public static bool 		DoingCollectible = false;   
		public static bool 		IndirectCall = false;  		
    	public static string	RandomPresellSKU;    
    	public static bool		GetRandomPresellSKUFailed;     	
    	public static string 	IPOSVersion;    
    	public static string 	RetechVersion;        	
    	public static string 	OSVersion;       	
    	public static string 	RegisterNumber;
    	public static string 	RegisterName;
    	public static bool		IsMaster;
    	public static bool		PreOrderFailed;    	
    	public static bool		Flush;     
    	public static bool		F12Flush;  
    	public static bool		POSBrowserFlush;      	
    	public static int 	 	NumberOfRegisters;
    	public static string 	Register1DriveLetter;
    	public static int 		RecordsFound;    	
    	public static string 	RecordsFoundString;
    	public static string 	LoyaltyNonLoyaltyFlag;    	// Equal to "Loyalty" or "NonLoyalty"
    	
    	public static string 	LoyaltyCardNum = "0000000000000";
    	
    	public static string	EnrollmentCardNumber = "";
    	public static string	EnrollmentFullCardNumber = "";
    		
    	public static string	CreditCardNumber;
    	public static string	CreditCardMonth;
    	public static string	CreditCardYear;
    	public static string	CreditCardZip;    	
    	public static string	PayWithMethod; // Cash, Credit, PURCC
    	public static int 		PayWithOffset = 1;       	
    	
		public static bool 		DoDumpStats;
    	public static string 	StatsFileName;
    	public static string 	StatsFileNameQ4;    	
    	public static string 	Q4StatLine; 
    	public static string 	Q4StatBuffer = "";     	
    	public static string	CurrentMetricDesciption;        	
    	public static string 	ErrorFileName;
    	public static string 	ErrorFileNameQ4;    	
    	public static string 	LogFileName;   
    	public static string	LogText;
    	public static int 		LogFileIndentLevel;    	
    	public static string	WavFilePath;
    	
    	public static int 		IterationsToDo;
    	public static string 	TimeToStopExecution;    
    	public static string 	TimeToStartExecution;        	
    	
    	public static bool 		FormOfPaymentNeeded;
    	
    	public static string 	ScenarioStartTime;
    	public static string	ScenarioEndTime;    	

    	// PAL Status Monitor Variables
    	public static bool 		MonitorStartVariablesInitialized = false;  

    	public static bool 		DBAvailable = true;      	
    	public static int 		ErrorsToday = 0;  
    	public static int 		ScenariosToday = 0;     	
    	public static int 		IterationsToday = 0; 
    	
    	public static int 		StartPOSMemory = 0;
    	public static int 		NowPOSMemory = 0;   
    	
    	public static int 		StartThreadCount = 0;
    	public static int 		NowThreadCount = 0;       	
    	
    	public static int 		StartGDIObjects = 0;   
    	public static int 		NowGDIObjects = 0;

    	public static int 		StartUserObjects = 0;   
    	public static int 		NowUserObjects = 0;
    	
    	public static int 		StartHandles = 0; 
    	public static int 		NowHandles = 0;
    	
    	public static float		NowCustomerLookup = 0.0F;
    	public static float		NowEnterSKU = 0.0F;
    	public static float		NowPayWithCash = 0.0F;
    	public static float		NowPayWithCredit = 0.0F;
    	public static float		NowPayWithPURCC = 0.0F;    	

    	// Get POS.exe Process Info variables
		public static int 		POSHandleCount = 0;
		public static int 		POSThreads = 0;
		public static int 		POSUsedMemory = 0;
		public static int 		POSGDIObjects = 0;
		public static int 		POSUserObjects = 0;
		public static int 		POSCurrentCPUUsage = 0;
        public static int		NumberOfTradesMinusOne = 0;	// For Scenarios 47 & 48 to set and pass to common code		

    	// Switches
    	public static bool 		SwitchPhoneNumbersNonLoyalty;
    	public static bool 		SwitchPhoneNumbersLoyalty;
    	public static bool 		SwitchAllRegistersUseAllPhoneNumbers;
    	public static bool 		SwitchScenario9Use40SKUs;
    	public static bool 		SwitchSkipCustomerLookup; 
    	public static bool 		SwitchMetricOverRide;  
    	public static bool 		SwitchQuitRunningOnError;      	
    	public static bool 		SwitchUploadOnly;
    	public static bool 		SwitchPauseBetweenScenariosOff;

 		public static bool 		LoopingDone = false;
 		public static bool 		StopByTime = false;    	
    	
    	public static int		PhoneMaxOffset = 0;
    	public static string 	NextPhoneNumber;
    	public static string 	EdgePowerUpNumber;    	
    	public static string 	OverridePhoneNumber;  
    	public static string 	OverrideSKU;      
    	public static string 	PhoneNumbertype;         	// Loyalty ("Pro", "Basic") Non-Loyalty ("NonMemberGenesis", "NonMemberProfile") or "" for all
    	public static string 	PhoneNumberMatchingCard;   
    	public static int		TotalNumberOfPhoneNumbers = 0;       	
	   	public static string[]	PhoneArrayLoyaltyPro = new string[300];
	   	public static string[]	PhoneArrayLoyaltyProCard = new string[300];	   	
	   	public static string[]	PhoneArrayLoyaltyBasic = new string[300];
	   	public static string[]	PhoneArrayLoyaltyBasicCard = new string[300];	   	
	   	public static string[]	PhoneArrayNonLoyaltyNonMemberGenesis = new string[300];
	   	public static string[]	PhoneArrayNonLoyaltyNonMemberProfile = new string[300];	
	   	
    	public static int		TotalNumberSimpleOneSKUs = 0; 
    	public static string 	SimpleOneSKUCashCreditToggle = "Cash";    	
    	public static int		SimpleOneSKUsOffset = 0;   	   	    	
	   	public static string[]	SimpleOneSku = new string[350];		 
	   	public static string[]	SimpleOneSkuDescription = new string[350];			   	

    	// Array for run flag for each scenario
    	public static bool[]	DoScenarioFlag = new bool[MaxScenarioNumber + 1];
		
    	// General use temporary
    	public static float		OverheadTime;
    	public static float		TempFloat;
    	public static float		TradeIn;    	
    	public static float		F12Total;
    	public static float		LastEnterToLogin;    	
    	public static float		F12toTotal;
    	public static float		Scenario9and10F5Total;
    	public static string	TempErrorString;
    	public static float		AdjustedTime;        	

    	//Metric Database variables
    	public static string 	TestType;
    	public static bool		IsPerformanceTest;
    	public static int		Test_ID;
    	public static int		AutoVer_ID;
    	public static int		Register_ID;
    	
    	
    	// Scenario stats
    	public static string 	Scenario3ItemLookup;
		public static string 	Scenario3RecordsFound;
		public static string 	Scenario3Total;
		
		public static string	Scenario4CustomerLookup;
		public static string	Scenario4CustomerLookupEnterToListTime;
		public static string	Scenario4F12CompleteTradein;
		public static string	Scenario4F12Total;
		public static string	Scenario4LastEnterToLogin;
		public static string	Scenario4Total;
					
		public static string	Scenario5CustomerLookup;
		public static string	Scenario5CustomerLookupEnterToListTime;
		public static string	Scenario5F12toTotal;
		public static string	Scenario5F5Total;
		public static string	Scenario5Total;

		public static string	Scenario6customerLookup;
		public static string	Scenario6CustomerLookupEnterToListTime;
		public static string	Scenario6F12Search;
		public static string	Scenario6RecordsFound;
		public static string	Scenario6AddTranaction;
		public static string	Scenario6F2Time;
		public static string	Scenario6Total;

		public static string	Scenario7CustomerLookup;
		public static string	Scenario7CustomerLookupEnterToListTime;
		public static string	Scenario7F12toTotal;
		public static string	Scenario7Total;
					
		public static string	Scenario8LoadBrowser;
		public static string	Scenario8AddtoCart;
		public static string	Scenario8FirstCheckOut;
		public static string	Scenario8ContinuetoShipping;
		public static string	Scenario8ContinuefromShipping;
		public static string	Scenario8SubmitOrder;
		public static string	Scenario8POSOrderLookup;
		public static string	Scenario8F12toTotal;
		public static string	Scenario8F5Total;
		public static string	Scenario8Total;
					
		public static string	Scenario9CustomerLookup;
		public static string	Scenario9CustomerLookupEnterToListTime;
		public static string	Scenario9Enter10SKUs;
		public static string	Scenario9F12toTotal;
		public static string	Scenario9F5Total;
		public static string	Scenario9Total;
					
		public static string	Scenario10CustomerLookup;
		public static string	Scenario10CustomerLookupEnterToListTime;
		public static string	Scenario10Enter10SKUs;
		public static string	Scenario10F12toTotal;
		public static string	Scenario10F5Total;
		public static string	Scenario10Total;
					
		public static string	Scenario11CustomerLookup;
		public static string	Scenario11CustomerLookupEnterToListTime;
		public static string	Scenario11F3Search;
		public static string	Scenario11Logout2ndF2Time;
		public static string	Scenario11Total;
					
		public static string	Scenario12LoadBrowser;	
		public static string	Scenario12RecommerceLink;
		public static string	Scenario12RecommerceSearch;
		public static string	Scenario12GIConversionApp;
		public static string	Scenario12WorkDay;
		public static string	Scenario12GoStores;
		public static string	Scenario12Total;
		
    	public static int		Scenario9ExtraSkusOffset;
    	
 		// Used by Scenario 9 and 10
    	public static string 	SKU1;
    	public static string 	SKU2;
    	public static string 	SKU3;
    	public static string 	SKU4;
    	public static string 	SKU5;
    	public static string 	SKU6;
    	public static string 	SKU7;
    	public static string 	SKU8;
    	public static string 	SKU9;
    	public static string 	SKU10;
    	
    	// Input data from InputData.csv
    	public static string 	S4Sku1;
    	public static string 	S5Sku1; 
    	public static string 	S5Sku2;   
    	public static string 	S8Sku;         	
    	
    	public static string 	S9SKU1;
    	public static string 	S9SKU2;
    	public static string 	S9SKU3;
    	public static string 	S9SKU4;
    	public static string 	S9SKU5;
    	public static string 	S9SKU6;
    	public static string 	S9SKU7;
    	public static string 	S9SKU8;
    	public static string 	S9SKU9;
    	public static string 	S9SKU10;     	
    	public static string 	S9SKU11;
    	public static string 	S9SKU12;
    	public static string 	S9SKU13;
    	public static string 	S9SKU14;
    	public static string 	S9SKU15;
    	public static string 	S9SKU16;
    	public static string 	S9SKU17;
    	public static string 	S9SKU18;
    	public static string 	S9SKU19;
    	public static string 	S9SKU20; 
    	public static string 	S9SKU21;
    	public static string 	S9SKU22;
    	public static string 	S9SKU23;
    	public static string 	S9SKU24;
    	public static string 	S9SKU25;
    	public static string 	S9SKU26;
    	public static string 	S9SKU27;
    	public static string 	S9SKU28;
    	public static string 	S9SKU29;
    	public static string 	S9SKU30; 
    	public static string 	S9SKU31;
    	public static string 	S9SKU32;
    	public static string 	S9SKU33;
    	public static string 	S9SKU34;
    	public static string 	S9SKU35;
    	public static string 	S9SKU36;
    	public static string 	S9SKU37;
    	public static string 	S9SKU38;
    	public static string 	S9SKU39;
    	public static string 	S9SKU40;    
    	
    	public static string 	S10SKU1;
    	public static string 	S10SKU2;
    	public static string 	S10SKU3;
    	public static string 	S10SKU4;
    	public static string 	S10SKU5;
    	public static string 	S10SKU6;
    	public static string 	S10SKU7;
    	public static string 	S10SKU8;
    	public static string 	S10SKU9;
    	public static string 	S10SKU10;   

    	public static string 	S11Sku1;
    	public static string 	S11Zip;   	
    	
    	public static string 	S14SKU;    	

// 	Q4 variables	

    	public static string 	S15SKU;	
    	public static string 	S15PostalCode;  		
    	
     	public static string 	S16SKU;	
    	
    	public static string 	S17SKU;
    	public static string 	S17PowerUpNumber;
    	public static string 	S17Trade1;   		
    	public static string 	S17Trade2;    	
    	public static string 	S17Trade3;    	
    	public static string 	S17Trade4;    	
    	public static string 	S17Trade5;  	
    	public static string 	S17Trade6;   		
    	public static string 	S17Trade7;    	
    	public static string 	S17Trade8;    	
    	public static string 	S17Trade9;    	
    	public static string 	S17Trade10;  	
    	public static string 	S17Trade11;   		
    	public static string 	S17Trade12;    	
    	public static string 	S17Trade13;    	
    	public static string 	S17Trade14;    	
    	public static string 	S17Trade15;  	
    	public static string 	S17Trade16;   		
    	public static string 	S17Trade17;    	
    	public static string 	S17Trade18;    	
    	public static string 	S17Trade19;    	
    	public static string 	S17Trade20;  	
    	public static string 	S17Trade21;   		
    	public static string 	S17Trade22;    	
    	public static string 	S17Trade23;    	
    	public static string 	S17Trade24;    	
    	public static string 	S17Trade25;  	   
    	
    	public static string 	S18SKU;  	    	
    	public static string 	S18SKUSerial; 
    	
    	public static string 	S19SKU;  	 
    	
    	public static string 	S20SKUa; 
    	public static string 	S20SKUb;     	
    	
     	public static string 	S21SKU;	

     	public static string 	S22SKU;	

     	public static string 	S23SKU;	
    	
     	public static string 	S24SKU;	
    	
     	public static string 	S25SKUa;	
     	public static string 	S25SKUb;	     	

     	public static string 	S26SKU;	
    	
     	public static string 	S27SKU;	
    	
     	public static string 	S28SKU;	
    	
     	public static string 	S29SKU;	
    	
     	public static string 	S30SKU;	
   	
    }         
}
