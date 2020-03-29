package com.mycompany.phonebook.bo;

import org.jbanana.core.Persistent;
import org.jbanana.rest.Restable;
import lombok.AllArgsConstructor;
import lombok.Data;

@Data 
@AllArgsConstructor
public class Phone implements Persistent,  Restable{

	private static final long serialVersionUID = 1L;
	
	private final String id;

	private String number;
	private Type type;

	public Phone(){id = null;}
	public Phone(String number, Type type) {
		this.id = null;
		this.number = number;
		this.type = type;
	}

	public static enum Type{CELLPHONE, HOME, OFFICE, OTHER}
	
	@Override public String xpathContainer() {return "contacts[@id=?]/phones";}
	@Override public String xpathElement() {return "contacts[@id=?]/phones[@id=?]";}
	@Override public String restContext() {	return "/phonebook/contacts/id/:id/phones";}
	@Override public boolean avoidDelete() {return false;}
}