package org.jbanana.rest;

import java.util.List;

import org.jbanana.core.Container;
import org.jbanana.core.Convetions.Singleton;

@Singleton
@SuppressWarnings("unchecked") 
public interface RestMapper {

	List<RestMap> inspectEntryPointsAndMap(Container container, Class<?>...entryPoints);
	List<RestMap> inspectEntryPointAndMap(Container container, Class<?> entryPoint);

	List<RestMap> inspectRestablesAndMap(Container container, Class<? extends Restable>...restables);
	List<RestMap> inspectRestableAndMap(Container container, Class<? extends Restable> restable);
}