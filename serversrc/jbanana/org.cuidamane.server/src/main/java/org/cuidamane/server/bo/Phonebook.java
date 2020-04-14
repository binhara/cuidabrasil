package org.cuidamane.server.bo;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import org.jbanana.core.Container;

import lombok.Getter;
import lombok.NoArgsConstructor;

@NoArgsConstructor
public class Phonebook implements Serializable {

	private static final long serialVersionUID = 1L;
	
	@Getter private final List<Contact> 		   contacts = new ArrayList<>();
	@Getter private final List<TokenAuthorization> tokens   = new ArrayList<>();
	

	public static Phonebook getPrevalentSystem() {
		return (Phonebook)Container.getInstances().get(Phonebook.class.getSimpleName()).getPrevalentSystem();
	}
	
}