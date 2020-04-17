package org.jbanana.log;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.Writer;
import java.nio.file.FileSystems;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import lombok.extern.slf4j.Slf4j;

@Slf4j
public class Logger {
	
	private static Logger instance;
	
	protected Logger() {}

	public static Logger getInstace() {
		if(instance == null)
			instance = new Logger();
		return instance;
	}
	
	private final List<ILogger> handlers = new ArrayList<>();
	private boolean writeLocalFile = false;
	
	private static final String FILE_NAME = "JB_log.txt";
	
	
	// ------------------- LOG METHODS
	public static void info(String message) {
		log.info(message);
		if(getInstace().writeLocalFile)
			writeFile(new Date().toString()+" - INFO  - "+message+"\n");
		sentToHandlers(message, LogType.Info);
	}
	
	public static void warning(String message) {
		log.warn(message);
		if(getInstace().writeLocalFile)
			writeFile(new Date().toString()+" - WARN  - "+message+"\n");
		sentToHandlers(message, LogType.Warn);
	}

	public static void error(String message) {
		log.error(message);
		if(getInstace().writeLocalFile)
			writeFile(new Date().toString()+" - ERROR - "+message+"\n");
		sentToHandlers(message, LogType.Error);
	}
	
	public static void debug(String message) {
		log.info("[DEBUG] - "+message);
		if(getInstace().writeLocalFile)
			writeFile(new Date().toString()+" - DEBUG - "+message+"\n");
		sentToHandlers(message, LogType.Debug);
	}
	
	/**
	 * message_built = message+"\nEXCEPTION: "+e.getMessage()+"\n\nStackTrace: "+StackTraceUtil.getStackTrace(e)+"\n"
	 * @param message
	 * @param e
	 */
	public static void exception(String message, Throwable e) {
		String msg = message+"\nEXCEPTION: "+e.getMessage()+"\n\nStackTrace: "+StackTraceUtil.getStackTrace(e)+"\n"; 
		log.error(msg);
		if(getInstace().writeLocalFile)
			writeFile(new Date().toString()+" - "+msg+"\n");
		sentToHandlers(msg, LogType.Exception);
	}
	
	private static void sentToHandlers(String message, LogType type) {
		if(getInstace().handlers.isEmpty()) return;
		for (ILogger handler : getInstace().handlers)
			try { handler.handler(message, type);
			} catch (Exception e) {
				e.printStackTrace();
				writeFile("------------------------------------------------------------------------\nException Message: "+e.getMessage());
				writeFile("Exception StackTrace: "+StackTraceUtil.getStackTrace(e));
			}
	}
	
	
	// ------------------- VARIABLE METHODS
	
	public static void setWriteinLocalFile(boolean writeLocalFile) {
		getInstace().writeLocalFile = writeLocalFile;
	}
	
	public static void addLogHandler(ILogger handler) {
		if(handler == null) return;
		getInstace().handlers.add(handler);
	}

	// ------------------- CLASS METHODS
	
	public static void startLogger() {
		
		getInstace();
		
		File logFile = new File(getLogFile());
		
		if(logFile.exists() && logFile.isFile())
			return;
		
		try {
			logFile.createNewFile();
		} catch (IOException e) {
			log.error("Logger erro: " + e.getMessage());
		}
	}
	
	private static String getLogFile() {
		//File file = FileSystems.getDefault().getPath("", FILE_NAME).toFile();	
		return FileSystems.getDefault().getPath("", FILE_NAME).toString();
	}
	
	private static void writeFile(String message) {
		Writer file;
		try {
			file = new BufferedWriter(new FileWriter(getLogFile(), true));
			file.append(message);
			file.close();
		} catch (IOException e) {
			log.error("Logger error: " + e.getMessage());
		}
	}
	
}
