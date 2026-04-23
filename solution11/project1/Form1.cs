using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace project1
{
    public partial class Form1 : Form
    {
        // قائمة الطلاب
        List<Student> students = new List<Student>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // ➕ زر الإضافة
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out int id) &&
                double.TryParse(txtGPA.Text, out double gpa))
            {
                Student s = new Student()
                {
                    Id = id,
                    Name = txtName.Text,
                    GPA = gpa
                };

                students.Add(s); // إضافة الطالب للقائمة

                // --- هذا السطر هو السر لإظهار البيانات في الجدول ---
                UpdateGrid();

                MessageBox.Show("تمت الإضافة");
            }
            else
            {
                MessageBox.Show("تأكد من إدخال بيانات صحيحة");
            }
        }

        private void UpdateGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = students;
        }
        // ❌ زر الحذف
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 1. نتأكد أن هناك صفاً مختاراً في الجدول
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // 2. نجلب الطالب من الصف المختار
                var selectedRow = dataGridView1.SelectedRows[0];
                var studentToDelete = (Student)selectedRow.DataBoundItem;

                // 3. نحذفه من القائمة
                students.Remove(studentToDelete);

                // 4. نحدث الجدول (باستخدام الدالة التي أنشأناها)
                UpdateGrid();

                MessageBox.Show("تم حذف الطالب بنجاح");
            }
            else
            {
                MessageBox.Show("يرجى اختيار طالب من الجدول أولاً");
            }
        }
        // 🔍 زر البحث
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 1. نتأكد أن المستخدم أدخل رقماً في صندوق الـ ID
            if (int.TryParse(txtId.Text, out int id))
            {
                // 2. نبحث عن الطالب في القائمة
                var student = students.Find(s => s.Id == id);

                if (student != null)
                {
                    // 3. إذا وجدناه، نعرض هذا الطالب فقط في الجدول
                    List<Student> searchResult = new List<Student> { student };
                    dataGridView1.DataSource = searchResult;
                }
                else
                {
                    // 4. إذا لم نجده، نظهر رسالة ونفرغ الجدول
                    MessageBox.Show("عذراً، الطالب غير موجود!");
                    dataGridView1.DataSource = null;
                }
            }
            else
            {
                MessageBox.Show("يرجى إدخال رقم ID صحيح للبحث");
            }
        }
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            dataGridView1.DataSource = students.Select(s => new
            {
                s.Id,
                s.Name,
                s.GPA
            }).ToList();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];

                MessageBox.Show(
                    $"ID: {row.Cells[0].Value}\nName: {row.Cells[1].Value}\nGPA: {row.Cells[2].Value}"
                );
            }
        }
    }
}