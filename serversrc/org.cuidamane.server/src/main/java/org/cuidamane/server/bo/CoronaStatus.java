package org.cuidamane.server.bo;

import org.cuidamane.server.Clock;
import org.jbanana.core.Persistent;
import org.jbanana.rest.Restable;

import lombok.Data;

@Data
public class CoronaStatus implements Persistent, Restable{

	private static final long serialVersionUID = 1L;
	public static enum Status{UNLOCKED, ISOLATED, QUARANTINED}
	
	private String id;
	public String getId() {return id;	}
	public CoronaStatus(){
		this.id = null;
		this.status = Status.UNLOCKED;
		this.timestamp = Clock.now();
	}
	
	private long timestamp;
	private Status status;
	
	public CoronaStatus(Status status) {
		this.timestamp = Clock.now();
		this.status = status;
	}
	
	@Override public String xpathContainer() {return "contacts[@id=?]/journal";}
	@Override public String xpathElement() {return "contacts[@id=?]/journal[@id=?]";}
	@Override public String restContext() {	return "/phonebook/contacts/id/:id/journal";}
	@Override public boolean avoidDelete() {return true;}
}
