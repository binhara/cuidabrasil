package com.mycompany.phonebook.bo;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;
import lombok.Getter;
import lombok.NoArgsConstructor;

@NoArgsConstructor
public class Phonebook implements Serializable{

	private static final long serialVersionUID = 1L;
	
	@Getter private final List<Contact> contacts = new ArrayList<>();	
}