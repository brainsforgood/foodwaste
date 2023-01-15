import React from "react";
import {
  StyleSheet,
  Text,
  View,
  SafeAreaView,
  SectionList,
  StatusBar,
} from "react-native";

class Product {
  id: number;
  name: string;
  expiryDate: Date;

  constructor(id: number, name: string, expiryDate: Date = new Date()) {
    this.id = id;
    this.name = name;
    this.expiryDate = expiryDate;
  }
}

const NewProduct = (id: number, name: string, expiryDate: Date = new Date()) => {
  return new Product(id, name, expiryDate);
};

const DATA = [
  {
    title: "Pantry",
    data: [
      NewProduct(1, "Black Beans", new Date(Date.now() + 259200000)),
      NewProduct(2, "Cookies"),
      NewProduct(3, "Rice"),
    ],
  },
  {
    title: "Fridge",
    data: [
      NewProduct(4, "Chicken"),
      NewProduct(5, "Cucumber"),
      NewProduct(6, "Milk"),
      NewProduct(6, "Paprika"),
    ],
  },
];

const App = () => (
  <SafeAreaView style={styles.container}>
    <SectionList
      sections={DATA}
      keyExtractor={(item) => item.id.toString()}
      renderItem={({ item }) => (
        <View style={styles.item}>
          <Text style={styles.title}>Product: {item.name}</Text>
          <Text style={styles.title}>
            Exp: {item.expiryDate.getDate()}-{item.expiryDate.getMonth() + 1}-{item.expiryDate.getFullYear()}
          </Text>
        </View>
      )}
      renderSectionHeader={({ section: { title } }) => (
        <Text style={styles.header}>{title}</Text>
      )}
    />
  </SafeAreaView>
);

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingTop: StatusBar.currentHeight,
    marginHorizontal: 16,
  },
  item: {
    backgroundColor: "#f9c2ff",
    padding: 20,
    marginVertical: 8,
  },
  header: {
    fontSize: 32,
    backgroundColor: "#fff",
  },
  title: {
    fontSize: 24,
  },
});

export default App;
