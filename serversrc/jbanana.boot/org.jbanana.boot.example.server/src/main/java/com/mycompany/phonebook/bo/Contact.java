package com.mycompany.phonebook.bo;

import java.util.ArrayList;
import java.util.List;

import org.jbanana.core.Persistent;
import org.jbanana.rest.Restable;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data 
@AllArgsConstructor
public class Contact implements Persistent, Restable{

	private static final long serialVersionUID = 1L;

	private final String id;
	private final List<Phone> phones = new ArrayList<>(); 

	private String firstName;
	private String surname;
	
	public Contact(){id = null;}
	public Contact(String firstName, String surname) {
		this.id = null;
		this.firstName = firstName;
		this.surname = surname;
	}

	@Override public String xpathContainer() {return "contacts";}
	@Override public String xpathElement() {return "contacts[@id=?]";}
	@Override public String restContext() {	return "/phonebook/contacts";}
	@Override public boolean avoidDelete() {return false;}
}