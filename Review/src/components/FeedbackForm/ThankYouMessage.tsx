import './FeedbackForm.css';

interface ThankYouMessageProps {
  name: string;
  onReset: () => void;
}

export const ThankYouMessage = ({ name, onReset }: ThankYouMessageProps) => (
  <div className="thank-you-message">
    <h2>Спасибо, {name}!</h2>
    <p>Ваш отзыв поможет нам стать лучше.</p>
    <button onClick={onReset} className="submit-btn">
      Оставить новый отзыв
    </button>
  </div>
);