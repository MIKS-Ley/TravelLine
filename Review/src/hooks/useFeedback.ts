import { useState, useEffect } from 'react';
import type { Feedback } from '../types/feedback';

export const useFeedback = () => {
  const [feedbacks, setFeedbacks] = useState<Feedback[]>([]);

  useEffect(() => {
    const savedFeedbacks = localStorage.getItem('feedbacks');
    if (savedFeedbacks) {
      setFeedbacks(JSON.parse(savedFeedbacks));
    }
  }, []);

  const saveFeedback = (newFeedback: Omit<Feedback, 'id' | 'date'>) => {
    const feedbackToSave: Feedback = {
      ...newFeedback,
      id: Date.now().toString(),
      date: new Date().toISOString(),
    };

    const updatedFeedbacks = [...feedbacks, feedbackToSave];
    setFeedbacks(updatedFeedbacks);
    localStorage.setItem('feedbacks', JSON.stringify(updatedFeedbacks));
    
    return feedbackToSave;
  };

  return { feedbacks, saveFeedback };
};