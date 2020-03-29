package org.jbanana.rest;

import static org.junit.Assert.assertEquals;

import java.io.Serializable;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.jbanana.core.Container;
import org.jbanana.core.Convetions.Crud;
import org.jbanana.core.Convetions.Rest;
import org.jbanana.core.Persistent;

import lombok.AllArgsConstructor;

public class EntryPointMapperTest {

	//XXX: @Test fix this
	public void testMapMethodsToPath() {
		
		Container c = new Container("test");
		RestMapper mapper = c.my(RestMapper.class);
		
		Class<?> clazz = MockEntryPoint.class;
		List<RestMap> methods = mapper.inspectEntryPointAndMap(c, clazz);
		Map<String, RestMap> map = new HashMap<>();
		for (RestMap restmap : methods) 
			map.put(restmap.getEntryPointClass().getSimpleName() 
			   + "." + restmap.getMethod().getName(), restmap);
		
		checkMapping(map, "create", "/context/mockobjects", Rest.post, Crud.create, clazz);
		checkMapping(map, "restoreAll", "/context/mockobjects", Rest.get, Crud.restore, clazz);
		checkMapping(map, "restoreByName", "/context/mockobjects/name/:name", Rest.get, Crud.restore, clazz);
		checkMapping(map, "restoreById", "/context/mockobjects/id/:id", Rest.get, Crud.restore, clazz);
		checkMapping(map, "update", "/context/mockobjects", Rest.patch, Crud.update, clazz);
		checkMapping(map, "replace", "/context/mockobjects", Rest.put, Crud.update, clazz);
		checkMapping(map, "deleteById", "/context/mockobjects/id/:id", Rest.delete, Crud.delete, clazz);
		checkMapping(map, "deleteAll", "/context/mockobjects", Rest.delete, Crud.delete, clazz);
		
		assertEquals(8, methods.size());
		
		clazz = MockEntryPoint2.class;
		methods = mapper.inspectEntryPointAndMap(c, clazz);
		map = new HashMap<>();
		for (RestMap restmap : methods) 
			map.put(restmap.getEntryPointClass().getSimpleName() 
				+ "." + restmap.getMethod().getName(), restmap);
		
		checkMapping(map, "create", "/context/mockobjects", Rest.post, Crud.create, clazz);
		checkMapping(map, "restoreAll", "/context/mockobjects", Rest.get, Crud.restore, clazz);
		checkMapping(map, "restoreByName", "/context/mockobjects/name/:name", Rest.get, Crud.restore, clazz);
		checkMapping(map, "restoreById", "/context/mockobjects/id/:id", Rest.get, Crud.restore, clazz);
		checkMapping(map, "update", "/context/mockobjects", Rest.patch, Crud.update, clazz);
		checkMapping(map, "replace", "/context/mockobjects", Rest.put, Crud.update, clazz);
		checkMapping(map, "deleteById", "/context/mockobjects/id/:id", Rest.delete, Crud.delete, clazz);
		checkMapping(map, "deleteAll", "/context/mockobjects", Rest.delete, Crud.delete, clazz);
	}

	private void checkMapping(Map<String, RestMap> map, String name, String path, Rest rest, Crud crud, Class<?> clazz) {
		RestMap rm = map.get(clazz.getSimpleName() + "." + name);
		assertEquals(rest, rm.getRest());
		assertEquals(crud, rm.getCrud());
		assertEquals(path, rm.getPath());
		assertEquals(clazz, rm.getEntryPointClass());
	}
	
	public static class MockObject implements Persistent{
		
		private static final long serialVersionUID = 1L;
		
		@Override public String getId() {return null;}
	}
	
	@AllArgsConstructor
	@EntryPoint(restContext="/context/mockobjects/", targetClass=MockObject.class)
	public static class MockEntryPoint{
		
		public MockObject create(MockObject contact){return null;} 		// POST 	| CREATE  	| /context/mockobjects
		public MockObject[] restoreAll(){return null;} 								//GET 		| RESTORE 	| /context/mockobjects/
		public MockObject[] restoreByName(String name){return null;}		//GET 		| RESTORE 	| /context/mockobjects/name/:name
		public MockObject restoreById(String id){return null;}			   		//GET 		| RESTORE 	| /context/mockobjects/id/:id
		public MockObject update(MockObject contact){return null;}		//PATCH 	| UPDATE  	| /context/mockobjects
		public MockObject replace(MockObject contact){return null;}    	//PUT 		| UPDATE  	| /context/mockobjects
		public MockObject deleteById(String id) {return null;} 				//DELETE 	| DELETE 	| /context/mockobjects/id/:id
		public void deleteAll() {} 																//DELETE 	| DELETE 	| /context/mockobjects
		public void fooAll(){}
	}
	
	public static class MockEntryPoint2 extends RestEntryPoint<MockObject>{

		public MockEntryPoint2(Serializable sr, Date now ){
			super(sr, now,
					"contacts",
					"contacts[id=?]",
					"/context/mockobjects/",
					MockObject.class
			);	
		}
		
		public MockObject[] restoreByName(String name){return null;}		//GET 		| RESTORE 	| /context/mockobjects/name/:name
	}	
}