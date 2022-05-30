using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI_LIB {
    public class DependencyCycleError {
        public List<Type> typeFields;
        public List<Type> dependencies;

        public DependencyCycleError() {
            typeFields = new List<Type>();
            dependencies = new List<Type>();
        }

        public string CheckForDependencyCycles(Type objType) {
            if (IsContainDependencyCycle(objType)) {
                string errorStr = "Dependency cycle: " + dependencies.Last().Name;
                foreach (var dependency in dependencies) {
                    errorStr = "<-" + dependency.Name;
                }
                return errorStr;
            }
            return null;
        }

        public bool IsContainDependencyCycle(Type objType) {
            typeFields.Add(objType);
            FieldInfo[] objFields = objType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            List<Type> fieldsList = new List<Type>();
            foreach (FieldInfo field in objFields) {
                if (field.FieldType.IsClass) {
                    if (!fieldsList.Contains(field.FieldType))
                        fieldsList.Add(field.FieldType);
                }
            }

            foreach (var field in fieldsList) {
                if (!typeFields.Contains(field)) {
                    if (IsContainDependencyCycle(field)) {
                        dependencies.Add(field);
                        return true;
                    }
                } else {
                    dependencies.Add(field);
                    return true;
                }
            }
            return false;
        }
    }
}