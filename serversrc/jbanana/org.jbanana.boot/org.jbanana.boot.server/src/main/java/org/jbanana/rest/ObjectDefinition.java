package org.jbanana.rest;

import static org.jbanana.core.Convetions.PKG_KEY_BO;

import java.lang.reflect.Field;

import org.jbanana.core.Container;

import lombok.Data;

@Data
public class ObjectDefinition {

	private final Class<?> targetClass;
	private final String entityName;
	private final Field[] properties;
	
	public ObjectDefinition(Container container, Class<?> targetClass) {
		this.targetClass = targetClass;
		String pack = container.getConventions().get(PKG_KEY_BO);
		this.entityName = targetClass.getName().replace(pack + ".", "");
		properties = targetClass.getDeclaredFields();
	}
}
