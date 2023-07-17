from flask import Flask, request
import test_model

app = Flask(__name__)

@app.route('/predict_difficulty', methods=['POST'])
def predict_difficulty():
    data = request.get_json()
    print(data)
    prediction = test_model.predict_difficulty(data)
    return {"difficulty": int(prediction)}

if __name__ == '__main__':
    app.run(port=5000)
