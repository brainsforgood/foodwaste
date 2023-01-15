import * as React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { ProductScreen, PantryScreen } from "./components";

const Tab = createBottomTabNavigator();

export default function App() {
  return (
    <NavigationContainer>
      <Tab.Navigator>
        <Tab.Screen name="Product" component={ProductScreen} />
        <Tab.Screen name="Pantry" component={PantryScreen} />
      </Tab.Navigator>
    </NavigationContainer>
  );
}
