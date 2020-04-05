package org.cuidamane.server.bo;

import java.util.ArrayList;
import java.util.List;

import org.cuidamane.server.bo.CoronaStatus.Status;
import org.jbanana.core.Persistent;
import org.jbanana.rest.Restable;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import lombok.AllArgsConstructor;
import lombok.EqualsAndHashCode;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@ToString
@EqualsAndHashCode
@JsonIgnoreProperties
@AllArgsConstructor
public class Contact implements Persistent, Restable {

	private static final long serialVersionUID = 1L;
	
	@SuppressWarnings("unused")
	private String id;
	
	@Getter @Setter private String phone; 
	@Getter @Setter private String name;
	@Getter @Setter private int age;
	@Getter private final List<CoronaStatus> journal = new ArrayList<>();

	@Override
	public String getId() {
		return phone;
	}		
	
	public Contact(){
		this.id = null;
		this.journal.add(new CoronaStatus(Status.UNLOCKED));
	}
	
	public Contact(String phone, String name, int age) {
		this.phone = phone;
		this.name = name;
		this.age = age;
		this.journal.add(new CoronaStatus(Status.UNLOCKED));
	}
	
	@Override public String xpathContainer() {return "contacts";}
	@Override public String xpathElement() {return "contacts[@id=?]";}
	@Override public String restContext() {	return "/phonebook/contacts";}
	@Override public boolean avoidDelete() {return true;}
}