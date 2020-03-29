package org.cuidamane.server;

import org.cuidamane.server.bo.Phonebook;
import org.jbanana.core.Container;
import org.prevayler.Prevayler;

public class Clock {
	
	public static long now() {
		Container container = Container.getContainer(Phonebook.class);
		Prevayler prevayler = container.getPrevayler();
		if(prevayler==null) return 0;
		
		org.prevayler.Clock clock = prevayler.clock();
		return clock.time().getTime();
	}
}
