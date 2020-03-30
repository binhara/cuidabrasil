package org.cuidamane.server.bo;

import java.io.Serializable;
import java.util.HashSet;
import java.util.Set;
import lombok.Getter;
import lombok.NoArgsConstructor;

@NoArgsConstructor
public class Phonebook implements Serializable{

	private static final long serialVersionUID = 1L;
	
	@Getter private final Set<Contact> contacts = new HashSet<>();
}