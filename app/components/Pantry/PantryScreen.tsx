import { useState } from "react";
import {
  StyleSheet,
  Text,
  View,
  SafeAreaView,
  Pressable,
  StatusBar,
  FlatList,
  Modal,
  Alert,
} from "react-native";
import { Product } from "./Product";
import { BarcodeScanner } from "../BarcodeScanner/BarcodeScanner";

const EmptyPantry = [
  {
    title: "Pantry",
    data: [
      // new Product(1, "Black Beans", new Date(Date.now() + 259200000)),
      // new Product(2, "Cookies"),
      // new Product(3, "Rice"),
    ],
  },
];

export const PantryScreen = () => {
  const [modalVisible, setModalVisible] = useState(false);
  const [pantryList, setPantryList] = useState([
    // new Product(1, "blauwe kaas"),
    // new Product(2, "cookies"),
    // new Product(3, "rice"),
  ]);

  const AddToPantry = (productName) => {
    pantryList.push(new Product(pantryList.length, productName));
  };

  return (
    <>
      <Modal
        animationType="slide"
        transparent={true}
        visible={modalVisible}
        onRequestClose={() => {
          setModalVisible(!modalVisible);
        }}
      >
        <View style={styles.centeredView}>
          <BarcodeScanner dataCallback={(data) => AddToPantry(data)} />
          <Pressable
            style={[styles.button, styles.buttonClose]}
            onPress={() => setModalVisible(!modalVisible)}
          >
            <Text style={styles.textStyle}>Hide Modal</Text>
          </Pressable>
        </View>
      </Modal>

      <SafeAreaView style={styles.container}>
        <Pressable
          style={[styles.button, styles.buttonOpen]}
          onPress={() => setModalVisible(true)}
        >
          <Text style={styles.textStyle}>Show Modal</Text>
        </Pressable>
        {pantryList.length == 0 ? (
          <Text style={styles.title}>Empty pantry 😭</Text>
        ) : (
          <FlatList
            data={pantryList}
            keyExtractor={(item) => item.id.toString()}
            renderItem={({ item }) => (
              <View style={styles.item}>
                <Text style={styles.title}>Product: {item.name}</Text>
                <Text style={styles.title}>
                  Exp: {item.expiryDate.getDate()}-
                  {item.expiryDate.getMonth() + 1}-
                  {item.expiryDate.getFullYear()}
                </Text>
              </View>
            )}
          />
        )}
      </SafeAreaView>
    </>
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
  centeredView: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    marginTop: 22,
  },
  modalView: {
    margin: 20,
    backgroundColor: "white",
    borderRadius: 20,
    padding: 35,
    alignItems: "center",
    shadowColor: "#000",
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.25,
    shadowRadius: 4,
    elevation: 5,
  },
  button: {
    borderRadius: 20,
    padding: 10,
    elevation: 2,
  },
  buttonOpen: {
    backgroundColor: "#F194FF",
  },
  buttonClose: {
    backgroundColor: "#2196F3",
  },
  textStyle: {
    color: "white",
    fontWeight: "bold",
    textAlign: "center",
  },
  modalText: {
    marginBottom: 15,
    textAlign: "center",
  },
});
