import React, { memo } from 'react';
import TaskItem from '../TaskItem/TaskItem';
import InsertPreview from '../InsertPreview/InsertPreview';
import type { Day, Task } from '../../types/taskTypes';
import styles from './DayColumn.module.css';

interface DayColumnProps {
  day: Day;
  tasks: Task[];
  onDragStart: (
    task: Task,
    clientX: number,
    clientY: number,
    offsetX: number,
    offsetY: number
  ) => void;
  dropIndex: number | null;
  draggingTaskId: string | null;
  columnRef: (el: HTMLDivElement | null) => void;
  taskRefs: React.MutableRefObject<{ [key: string]: HTMLDivElement | null }>;
}

const DayColumn: React.FC<DayColumnProps> = memo(({
  day,
  tasks,
  onDragStart,
  dropIndex,
  draggingTaskId,
  columnRef,
  taskRefs
}) => {
  const shouldShowEndPreview = dropIndex === tasks.length && tasks.length > 0;
  
  return (
    <div className={styles.day} ref={columnRef}>
      <h2>{day.title}</h2>
      <div className={styles.taskList}>
        {/* Индикатор в начале списка */}
        {dropIndex === 0 && <InsertPreview />}
        
        {tasks.map((task, index) => (
          <React.Fragment key={task.id}>
            <TaskItem
              task={task}
              isDragged={draggingTaskId === task.id}
              onDragStart={onDragStart}
              taskRef={(el) => (taskRefs.current[task.id] = el)}
            />
            {/* Индикатор после текущего элемента, если это не последний элемент */}
            {dropIndex === index + 1 && index !== tasks.length - 1 && <InsertPreview />}
          </React.Fragment>
        ))}
        
        {/* Индикатор в конце списка (только если не показывается другой индикатор) */}
        {shouldShowEndPreview && <InsertPreview />}
      </div>
    </div>
  );
});

export default DayColumn;