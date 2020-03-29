package org.jbanana.core;

import java.util.Date;
import java.util.HashMap;
import java.util.Map;

public class PrevaylerThreadClock implements Clock {

	private static Map<Thread, Date> time = new HashMap<>();
	
	public static void setCurrentTime(Date now) {
		time.put(Thread.currentThread(), now);
	}
	
	@Override
	public Date now() {
		return time.get(Thread.currentThread());
	}
}