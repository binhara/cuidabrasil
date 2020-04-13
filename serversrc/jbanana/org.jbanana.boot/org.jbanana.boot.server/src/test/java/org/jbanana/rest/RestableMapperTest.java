package org.jbanana.rest;

import static org.junit.Assert.assertEquals;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.jbanana.core.Container;
import org.jbanana.core.Convetions.Crud;
import org.jbanana.core.Convetions.Rest;
import org.junit.Test;

import lombok.AllArgsConstructor;

public class RestableMapperTest {

	@Test
	@SuppressWarnings("unchecked")
	public void testMapMethodsToPath() {
		Container c = new Container(RestableMapperTest.class);
		RestMapper mapper = c.my(RestMapper.class);
		
		Class<? extends Restable> clazz = MockRestable.class;
		List<RestMap> methods = mapper.inspectRestablesAndMap(c, clazz);
		Map<String, RestMap> map = new HashMap<>();
		for (RestMap restmap : methods) 
			map.put(restmap.getMethod().getName(), restmap);
		
		checkMapping(map, "create", "/context/mocks", Rest.post, Crud.create, clazz);
		checkMapping(map, "restoreAll", "/context/mocks", Rest.get, Crud.restore, clazz);
		checkMapping(map, "restoreById", "/context/mocks/id/:id", Rest.get, Crud.restore, clazz);
		checkMapping(map, "update", "/context/mocks", Rest.patch, Crud.update, clazz);
		checkMapping(map, "replace", "/context/mocks", Rest.put, Crud.update, clazz);
		checkMapping(map, "deleteById", "/context/mocks/id/:id", Rest.delete, Crud.delete, clazz);
		checkMapping(map, "deleteAll", "/context/mocks", Rest.delete, Crud.delete, clazz);
		
		assertEquals(7, methods.size());
	}

	private void checkMapping(Map<String, RestMap> map, String name, String path, Rest rest, Crud crud, Class<?> clazz) {
		RestMap rm = map.get(name);
		assertEquals(rest.name(), rm.getRest());
		assertEquals(crud.name(), rm.getCrud());
		assertEquals(path, rm.getPath());
		assertEquals(RestEntryPoint.class, rm.getEntryPointClass());
		assertEquals(MockRestable.class, clazz);
	}
	
	@AllArgsConstructor
	public static class MockRestable implements Restable{
		
		@Override public String xpathContainer() { return "mocks";}
		@Override public String xpathElement() { return "mocks[id=?]";}
		@Override public String restContext() {return "/context/mocks/";	}
		@Override public boolean avoidDelete() {return false;}
	}
}