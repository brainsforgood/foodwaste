import {
  StyleSheet,
  Text,
  View,
  SafeAreaView,
  SectionList,
  StatusBar,
} from "react-native";
import { Product } from "./Product";

const DATA = [
  {
    title: "Pantry",
    data: [
      new Product(1, "Black Beans", new Date(Date.now() + 259200000)),
      new Product(2, "Cookies"),
      new Product(3, "Rice"),
    ],
  },
  {
    title: "Fridge",
    data: [
      new Product(4, "Chicken"),
      new Product(5, "Cucumber"),
      new Product(6, "Milk"),
      new Product(7, "Paprika"),
      new Product(8, "fasdf"),
    ],
  },
];

export const PantryScreen = () => {
  return (
    <SafeAreaView style={styles.container}>
      <SectionList
        sections={DATA}
        keyExtractor={(item) => item.id.toString()}
        renderItem={({ item }) => (
          <View style={styles.item}>
            <Text style={styles.title}>Product: {item.name}</Text>
            <Text style={styles.title}>
              Exp: {item.expiryDate.getDate()}-{item.expiryDate.getMonth() + 1}-
              {item.expiryDate.getFullYear()}
            </Text>
          </View>
        )}
        renderSectionHeader={({ section: { title } }) => (
          <Text style={styles.header}>{title}</Text>
        )}
      />
    </SafeAreaView>
  );
};

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
