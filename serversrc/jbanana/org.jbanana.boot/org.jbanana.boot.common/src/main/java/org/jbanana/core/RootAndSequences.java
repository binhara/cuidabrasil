package org.jbanana.core;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;
import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class RootAndSequences implements Serializable{

	private static final long serialVersionUID = 1L;
	
	private Serializable prevalentSystem;
	private final Map<Class<?>, Long> sequences = new HashMap<>();
}