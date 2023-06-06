from fastapi import FastAPI
from pydantic import BaseModel
from transformers import GPTJForCausalLM, GPTJTokenizer

app = FastAPI()

class GPTRequest(BaseModel):
    input: str

class GPTResponse(BaseModel):
    output: str

class GPTChatHistory:
    def __init__(self):
        self.history = []
        self.model_name = "EleutherAI/gpt-neo-2.7B"
        self.tokenizer = GPTJTokenizer.from_pretrained(self.model_name)
        self.model = GPTJForCausalLM.from_pretrained(self.model_name)

    def add_message(self, message):
        self.history.append(message)

    def generate_response(self, input_text):
        input_ids = self.tokenizer.encode(input_text, return_tensors="pt")
        output = self.model.generate(input_ids, max_length=100)[0]
        output_text = self.tokenizer.decode(output, skip_special_tokens=True)
        return output_text

    def clear_history(self):
        self.history = []

chat_history = GPTChatHistory()

@app.post("/gpt-prompt")
def gpt_prompt(request: GPTRequest):
    input_text = request.input
    chat_history.add_message(input_text)
    response_text = chat_history.generate_response(input_text)
    chat_history.add_message(response_text)
    return GPTResponse(output=response_text)

@app.post("/clear-chat")
def clear_chat():
    chat_history.clear_history()
    return {"message": "Chat history cleared."}
