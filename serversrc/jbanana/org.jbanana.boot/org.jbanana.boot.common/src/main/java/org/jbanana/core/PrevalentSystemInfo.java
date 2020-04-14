package org.jbanana.core;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class PrevalentSystemInfo implements Serializable {

	private static final long serialVersionUID = 1L;
	private Serializable prevalentSystem;
	private List<Object> components = new ArrayList<>();

	public PrevalentSystemInfo(Serializable prevalentSystem) {
		this.prevalentSystem = prevalentSystem;
		this.components =  Collections.emptyList();
	}
}
