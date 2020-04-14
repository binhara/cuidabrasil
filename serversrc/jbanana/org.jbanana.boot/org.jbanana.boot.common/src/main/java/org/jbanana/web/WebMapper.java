package org.jbanana.web;

import java.util.List;

import org.jbanana.core.Container;
import org.jbanana.core.Convetions.Singleton;
import org.jbanana.rest.RestMap;
import org.jbanana.rest.Restable;
import org.jbanana.rpc.RPCMap;

@Singleton
@SuppressWarnings("unchecked") 
public interface WebMapper {

	List<RestMap> inspectEntryPointsAndMap(Container container, Class<?>...entryPoints);
	List<RestMap> inspectEntryPointAndMap(Container container, Class<?> entryPoint);

	List<RestMap> inspectRestablesAndMap(Container container, Class<? extends Restable>...restables);
	List<RestMap> inspectRestableAndMap(Container container, Class<? extends Restable> restable);

	List<RPCMap> inspectRPCsAndMap(Container container, Class<?>...rpcs);
	List<RPCMap> inspectRPCAndMap(Container container, Class<?> rpc);
}