package org.jbanana.xstream;

import java.lang.reflect.Field;
import java.lang.reflect.Modifier;
import java.util.Collection;
import java.util.List;
import java.util.Set;

import com.thoughtworks.xstream.converters.Converter;
import com.thoughtworks.xstream.converters.MarshallingContext;
import com.thoughtworks.xstream.converters.UnmarshallingContext;
import com.thoughtworks.xstream.io.HierarchicalStreamReader;
import com.thoughtworks.xstream.io.HierarchicalStreamWriter;

@SuppressWarnings("rawtypes") 
public class FieldConverter implements Converter {

        public boolean canConvert(Class clazz) {return clazz.equals(Field.class);}
        public Object unmarshal(HierarchicalStreamReader reader, UnmarshallingContext context) {return null;}

        public void marshal(Object value, HierarchicalStreamWriter writer, MarshallingContext context) {
        			Field field = (Field) value;
        			
                writer.addAttribute("name", field.getName());
                writer.addAttribute("isFinal", ""+Modifier.isFinal(field.getModifiers()));
                writer.addAttribute("isStatic", ""+Modifier.isStatic(field.getModifiers()));
                setFieldType(writer, field);
        }
        
		public static void setFieldType(HierarchicalStreamWriter writer, Field field) {
			
			if(field.getType().isEnum() ) {
				writer.startNode("type");
				writer.setValue("string");
				writer.endNode();

				writer.startNode("enum");
				Class<?> c = field.getType();
				Object enums[] = c.getEnumConstants();
				for (Object e : enums) {
					writer.startNode("item");
					writer.setValue(e.toString());
					writer.endNode();
				}		
				writer.endNode();
				return;
			}
			
			if(field.getType()==String.class) {
				writer.startNode("type");
				writer.setValue("string");
				writer.endNode();
				return;
			}		
			
			if(field.getType()==Integer.class 
			|| field.getType()==Long.class 			
			|| field.getType()==Short.class 			
			|| field.getType()==int.class 			
			|| field.getType()==long.class 
			|| field.getType()==short.class ) {
				writer.startNode("type");
				writer.setValue("integer");
				writer.endNode();
				return;
			}				
			
			if(field.getType()==Double.class 
			|| field.getType()==Float.class 
			|| field.getType()==double.class
			|| field.getType()==float.class) {
				writer.startNode("type");
				writer.setValue("number");
				writer.endNode();
				return;
			}				
			
			if(field.getType().isAssignableFrom(Collection.class) 
			|| field.getType().isAssignableFrom(List.class)
			|| field.getType().isAssignableFrom(Set.class)
			|| field.getType().isArray()) {
				writer.startNode("type");
				writer.setValue("array");
				writer.endNode();
				return;
			}		

			if(field.getType()==boolean.class 
			|| field.getType()==Boolean.class ) {
				writer.startNode("type");
				writer.setValue("boolean");
				writer.endNode();
				return;
			}

			writer.startNode("ref");
			writer.setValue(field.getType().getSimpleName());
			writer.endNode();
		}
}