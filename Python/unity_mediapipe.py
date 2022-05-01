from http import server
import cv2
from cvzone.HandTrackingModule import HandDetector
import mediapipe
import socket


#Parameters

width, height = 1280, 720

#Webcam
cap = cv2.VideoCapture(0)
cap.set(3,width)
cap.set(4,height)

#Hand Detector
detector = HandDetector(maxHands=2, detectionCon=0.8)

#Communication
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)  # ici UDP,SOCK_STREAM for TCP
serverAddressport = ("127.0.0.1",2001)  #mon ip : ethernet : 10.163.222.71 / wifi : 10.163.226.48  / localhost : 127.0.0.1


while True:
    success, img = cap.read()

    #Hands
    hands, img = detector.findHands(img)

    
    data = []
    #Landmark values - (x,y,z) * 21
    if hands:
        
        #First hand detected
        hand = hands[0]
        #Landmark list
        lmList = hand['lmList']

        # Fill data buffer
        for lm in lmList:
            data.extend([lm[0],  height-lm[1], lm[2]])

        if len(hands) == 2:
            #First hand detected
            hand = hands[1]
            #Landmark list
            lmList = hand['lmList']

            # Fill data buffer
            for lm in lmList:
                data.extend([lm[0],  height-lm[1], lm[2]])          

        print(data)
        sock.sendto(str.encode(str(data)), serverAddressport)
    

    img = cv2.resize(img, (0,0), 
    None, 0.5, 0.5)

    
    cv2.imshow("Image", img)
    cv2.waitKey(1)