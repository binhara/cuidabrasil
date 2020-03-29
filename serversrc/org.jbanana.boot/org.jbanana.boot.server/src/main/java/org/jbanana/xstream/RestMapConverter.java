package org.jbanana.xstream;

import java.lang.reflect.Field;
import java.lang.reflect.Modifier;
import java.util.ArrayList;
import java.util.List;

import org.jbanana.rest.RestMap;

import com.thoughtworks.xstream.converters.Converter;
import com.thoughtworks.xstream.converters.MarshallingContext;
import com.thoughtworks.xstream.converters.UnmarshallingContext;
import com.thoughtworks.xstream.io.HierarchicalStreamReader;
import com.thoughtworks.xstream.io.HierarchicalStreamWriter;

@SuppressWarnings("rawtypes") 
public class RestMapConverter implements Converter {
	
		private List<String> keys = new ArrayList<>();
		private boolean returnIsBoolean = false;

        public boolean canConvert(Class clazz) {return clazz.equals(RestMap.class);}
        public Object unmarshal(HierarchicalStreamReader reader, UnmarshallingContext context) {return null;}

        public void marshal(Object value, HierarchicalStreamWriter writer, MarshallingContext context) {

	        	RestMap rm = (RestMap) value;
	        	
	        	writer.addAttribute("entityName", rm.getEntityName());
	        	writer.addAttribute("crud",  rm.getCrud());
	        	writer.addAttribute("rest", rm.getRest());
        			
            newNode(writer, "path", rm.getPath());
            newNode(writer, "swaggerPath", convertToSwaggerPath(rm.getPath()));
            
            createRequestInfo(writer, rm);
            
    			writer.startNode("targetClass");
    			writer.addAttribute("returnIsArray", ""+rm.isReturnIsArray());
    			writer.addAttribute("returnIsBoolean", ""+returnIsBoolean);
			writer.setValue(rm.getTargetClass().getName());
			writer.endNode();   
        }
		
		private void createRequestInfo(HierarchicalStreamWriter writer, RestMap rm) {
			
			boolean requestNodeCreated = false;
			boolean fieldsNodeCreated = false;
			try {
				
				if(keys.size()>0)
					requestNodeCreated = createRequestNodeIfNecessary(writer, requestNodeCreated);
				
				for (String key : keys) {
					writer.startNode("key");
					writer.addAttribute("name", key);
					writer.endNode();   
				}

				if(rm.getRest().equals("get")) return;
				
				if(rm.getRest().equals("delete")){
					returnIsBoolean = rm.getMethod().getReturnType() == boolean.class;
					return;
				}
				
				if(rm.getRest().equals("post")){
				    Field[] declaredFields = rm.getTargetClass().getDeclaredFields();
				    for (Field field : declaredFields) {
						if(Modifier.isStatic(field.getModifiers())) continue;
						if(Modifier.isFinal(field.getModifiers())) continue;
						requestNodeCreated = createRequestNodeIfNecessary(writer, requestNodeCreated);
						fieldsNodeCreated = createFieldsNodeIfNecessary(writer, fieldsNodeCreated);
						newFieldNode(writer, field); 
				    }  
				}
				
				if(rm.getRest().equals("put") || rm.getRest().equals("patch")){
				    Field[] declaredFields = rm.getTargetClass().getDeclaredFields();
				    for (Field field : declaredFields) {
						if(Modifier.isStatic(field.getModifiers())) continue;
						requestNodeCreated = createRequestNodeIfNecessary(writer, requestNodeCreated);
						fieldsNodeCreated = createFieldsNodeIfNecessary(writer, fieldsNodeCreated);
						newFieldNode(writer, field); 
				    }  
				}
				
			} finally {  
				if(fieldsNodeCreated) writer.endNode();
				if(requestNodeCreated) writer.endNode(); 
			} 
		}
		private boolean createFieldsNodeIfNecessary(HierarchicalStreamWriter writer, boolean fieldsNodeCreated) {
			if(!fieldsNodeCreated){
				writer.startNode("fields");
				fieldsNodeCreated = true;
			}
			return fieldsNodeCreated;
		}
		private boolean createRequestNodeIfNecessary(HierarchicalStreamWriter writer, boolean requestNodeCreated) {
			if(!requestNodeCreated){
				writer.startNode("request");
				requestNodeCreated = true;
			}
			return requestNodeCreated;
		}
        
		private void newFieldNode(HierarchicalStreamWriter writer, Field field) {
			writer.startNode("field");
			writer.addAttribute("name", field.getName());
			FieldConverter.setFieldType(writer, field);
			writer.endNode();
		}
        
		private void newNode(HierarchicalStreamWriter writer, String name, String value) {
			writer.startNode(name);
			writer.setValue(value);
            writer.endNode();
		}
		
		private String convertToSwaggerPath(String path) {		
        		keys.clear();	
			int counter = 1;
			while(path.contains(":id")){
				String name = "id" + counter;
				keys.add(name);
				path = path.replaceFirst(":id", "{" + name + "}");
				counter++;
			}
			
			return path;
		}
}