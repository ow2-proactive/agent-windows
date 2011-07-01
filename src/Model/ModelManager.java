/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package Model;

import Connections.Connections;
import GUI.GUIEditorWindows;
import Planning.Events;
import Utils.ApplicationLauncher;
import Utils.FileLocator;
import Utils.XMLMaker;
import Utils.XMLParser;
import java.io.File;
import java.util.ArrayList;
import javax.swing.JOptionPane;


/**
 *
 * @author pgouttef
 */
public class ModelManager {
    
    /**
     * The Configs Files by default : XML & XSD
     */
    private static String XMLFileName = "XML" + File.separatorChar + "PAAgent-config.xml";
    private static String XSDFileName4Windows = "XML" + File.separatorChar + "agent-windows.xsd";
    private static String XSDFileName4Linux = "XML" + File.separatorChar + "agent-linux.xsd";
    
    /**
     * The XML builder and parser
     */
    private static XMLParser xmlParser = new XMLParser();
    private static XMLMaker xmlBuilder = new XMLMaker();
    
    /**
     * Variables of application's model
     */
    private static String PROACTIVEHOME = "";
    private static String JAVAHOME = "";
    private static String SCRIPTONEXIT = "";
    private static String PROTOCOL = "";
    private static int NBRUNTIMES = 0;
    private static int MEMORYLIMIT = 0;
    private static int CPUUSAGE = 1;
    private static String PROCESSPRIORITY = "";
    private static int CLASSDATA = 1;
    private static int PORTMIN = 0;
    private static int PORTMAX = 65534;
    
    /**
     * List of NetworkInterfaces
     */
    private static ArrayList<String> NETWORKINTERFACESLIST = null;
    
    /**
     * The Events structure:
     *          ArrayList<Event> : list of events in the Planning panel
     */
    private static Events EVENTS = new Events();
    
    /**
     * The Connections structure:
     *          
     */
    private static Connections CONNECTIONS = new Connections();
    
    /**
     * The JVM Options
     */
    private static ArrayList<String> JVMOPTIONS = new ArrayList<String> ();
    
    /**
     * Tools
     */
    private static FileLocator fileLocator = new FileLocator();
    private static ApplicationLauncher appLauncher = new ApplicationLauncher();
    
    
    //GETTERS SETTERS
    public static String getJAVAHOME() { return JAVAHOME; }
    public static void setJAVAHOME(String JAVAHOME) { ModelManager.JAVAHOME = JAVAHOME; }
    public static String getPROACTIVEHOME() { return PROACTIVEHOME; }
    public static void setPROACTIVEHOME(String PROACTIVEHOME) { ModelManager.PROACTIVEHOME = PROACTIVEHOME; }
    public static String getXMLFileName() { return XMLFileName; }
    public static void setXMLFileName(String fileName) { ModelManager.XMLFileName = fileName; }
    public static String getXSDFileName() { return XSDFileName4Windows; }
    public static void setXSDFileName(String fileName) { ModelManager.XSDFileName4Windows = fileName; }
    public static String getSCRIPTONEXIT() { return SCRIPTONEXIT; }
    public static void setSCRIPTONEXIT(String SCRIPTONEXIT) { ModelManager.SCRIPTONEXIT = SCRIPTONEXIT; }
    public static Events getEVENTS() { return EVENTS; }
    public static void setEVENTS(Events EVENTS) { ModelManager.EVENTS = EVENTS; }
    public static int getMEMORYLIMIT() { return MEMORYLIMIT; }
    public static void setMEMORYLIMIT(int MEMORYLIMIT) { ModelManager.MEMORYLIMIT = MEMORYLIMIT; }
    public static int getNBRUNTIMES() { return NBRUNTIMES; }
    public static void setNBRUNTIMES(int NBRUNTIMES) { ModelManager.NBRUNTIMES = NBRUNTIMES; }
    public static String getPROTOCOL() { return PROTOCOL; }
    public static void setPROTOCOL(String PROTOCOL) { ModelManager.PROTOCOL = PROTOCOL; }
    public static Connections getCONNECTIONS() { return CONNECTIONS; }
    public static void setCONNECTIONS(Connections CONNECTIONS) { ModelManager.CONNECTIONS = CONNECTIONS; }
    public static String getPROCESSPRIORITY() { return PROCESSPRIORITY; }
    public static void setPROCESSPRIORITY(String PROCESSPRIORITY) { ModelManager.PROCESSPRIORITY = PROCESSPRIORITY; }
    public static int getCPUUSAGE() { return CPUUSAGE; }
    public static void setCPUUSAGE(int MAXCPUUSAGE) { ModelManager.CPUUSAGE = MAXCPUUSAGE; }
    public static ArrayList<String> getListInterfaces() { return NETWORKINTERFACESLIST; }
    public static void setListInterfaces(ArrayList<String> list) { ModelManager.NETWORKINTERFACESLIST = list; }
    public static ArrayList<String> getJVMOPTIONS() { return JVMOPTIONS; }
    public static void setJVMOPTIONS(ArrayList<String> JVMOPTIONS) { ModelManager.JVMOPTIONS = JVMOPTIONS; }
    public static ApplicationLauncher getAppLauncher() {  return appLauncher; }
    public static void setAppLauncher(ApplicationLauncher appLauncher) { ModelManager.appLauncher = appLauncher; }
    public static FileLocator getFileLocator() { return fileLocator; }
    public static void setFileLocator(FileLocator fileLocator) { ModelManager.fileLocator = fileLocator; }
    public static int getCLASSDATA() { return CLASSDATA; }
    public static void setCLASSDATA(int CLASSDATA) { ModelManager.CLASSDATA = CLASSDATA; }
    public static int getPORTMIN() { return PORTMIN; }
    public static void setPORTMIN(int PORTMIN) { ModelManager.PORTMIN = PORTMIN; }
    public static int getPORTMAX() { return PORTMAX; }
    public static void setPORTMAX(int PORTMAX) { ModelManager.PORTMAX = PORTMAX; }

    static {
        String os = System.getProperty("os.name").toLowerCase();
        if(os.indexOf( "win" ) >= 0) {
          PROCESSPRIORITY = "Idle";
       } else if (os.indexOf( "nix") >=0 || os.indexOf( "nux") >=0) {  
          PROCESSPRIORITY = "none";
       }
    }
    
    /**
     * Load xml file with validation
     */
    public static Boolean loadXML(GUIEditorWindows frame) {
        
        setJVMOPTIONS(  new ArrayList<String>() );
        
        String XSDFileName = "";
        String os = System.getProperty("os.name").toLowerCase();
       if(os.indexOf( "win" ) >= 0) {
          XSDFileName = XSDFileName4Windows;
       } else if (os.indexOf( "nix") >=0 || os.indexOf( "nux") >=0) {  
           XSDFileName = XSDFileName4Linux;
       }
        
        if(xmlParser.loadDocument(XSDFileName,XMLFileName)) {
            return true;
        } else {
            JOptionPane.showMessageDialog(frame,"The XML file is not valid.");
            return false;
        }
    }
    
    /**
     * push all datas into default XML file
     */
    public static Boolean saveXML(GUIEditorWindows frame) {
        if(xmlBuilder.saveDocument(XMLFileName)) {
            if(xmlParser.checkValidation(XMLFileName, XMLFileName))
            {
               JOptionPane.showMessageDialog(frame, "The file \"" + XMLFileName + "\" was saved.");
               return true; 
            } else {
               JOptionPane.showMessageDialog(frame,"The file " + XMLFileName + " is not valid.");
               return false;
            }
        } else {
            JOptionPane.showMessageDialog(frame,"An error was encountered.");
            return false;
        }
    }
    
    /**
     * push all datas into a specific XML file
     * @param saveAsFile : XML file
     */
    public static Boolean saveXML(GUIEditorWindows frame, String saveAsFile) {
        if(xmlBuilder.saveDocument(saveAsFile)) {
            if(xmlParser.checkValidation(saveAsFile, XMLFileName))
            {
               JOptionPane.showMessageDialog(frame,"The file \"" + saveAsFile + "\" was saved.");
               return true; 
            } else {
               JOptionPane.showMessageDialog(frame,"The file " + saveAsFile + " is not valid.");
               return false;
            }
        } else {
            JOptionPane.showMessageDialog(frame,"An error was encountered.");
            return false;
        }
    }
    
    public static void main(String[] args)
    {
        System.out.println("File name : " + XMLFileName);
        ModelManager.loadXML(null);
        System.out.println("ProActive : " + PROACTIVEHOME);
    }

    public static int convertDayToInt(String day) {
        
        if(day.equals("Monday")) { return 0; }
        else if(day.equals("Tuesday")) { return 1; }
        else if(day.equals("Wednesday")) { return 2; }
        else if(day.equals("Thursday")) { return 3; }
        else if(day.equals("Friday")) { return 4; }
        else if(day.equals("Saturday")) { return 5; }
        else if(day.equals("Sunday")) { return 6; }
        
        return 0;
    } 
    
    public static String convertIntToDay(int day) {
        
        if(day == 0) { return "Monday"; }
        else if(day == 1) { return "Tuesday"; }
        else if(day == 2) { return "Wednesday"; }
        else if(day == 3) { return "Thursday"; }
        else if(day == 4) { return "Friday"; }
        else if(day == 5) { return "Saturday"; }
        else if(day == 6) { return "Sunday"; }
        
        return "Monday";
    }
    
    public static int convertPriorityToInt(String priority) {
        
        String os = System.getProperty("os.name").toLowerCase();
        if(os.indexOf( "win" ) >= 0) {
            if(priority.equals("Idle")) { return 0; }
            else if(priority.equals("BelowNormal")) { return 1; }
            else if(priority.equals("Normal")) { return 2; }
            else if(priority.equals("AboveNormal")) { return 3; }
            else if(priority.equals("High")) { return 4; }
            else if(priority.equals("Realtime")) { return 5; }
            
        } else if (os.indexOf( "nix") >=0 || os.indexOf( "nux") >=0) {
            if(priority.equals("none")) { return 0; }
            else if(priority.equals("realtime")) { return 1; }
            else if(priority.equals("besteffort")) { return 2; }
            else if(priority.equals("idle")) { return 3; }
            
        }
        
        return 0;
    } 
    
    public static int convertProtocolToInt(String priority) {

        if(priority.equals("undefined")) { return 0; }
        else if(priority.equals("rmi")) { return 1; }
        else if(priority.equals("http")) { return 2; }
        else if(priority.equals("parm")) { return 3; }
        else if(priority.equals("pnp")) { return 4; }
        else if(priority.equals("pnps")) { return 5; }

        return 3;
    } 

    
}
